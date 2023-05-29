using BPLogix.BooksCvsGenerator.Domain.Requests;
using BPLogix.BooksCvsGenerator.Domain.Responses;
using BPLogix.BooksCvsGenerator.Domain.Shared;
using BPLogix.BooksCvsGenerator.Domain.Shared.Enums;
using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Http
{
    public class BooksHttpClient : HttpClient, IBooksApi
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheProvider _cacheProvider;
        public BooksHttpClient(
            HttpClient httpClient,
            ICacheProvider cacheProvider)
        {
            _httpClient = httpClient;
            _cacheProvider = cacheProvider;
        }

        public async Task<Result<List<ProcessBookResponse>>> ProcessBooksAsync(ProcessBookRequest request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string[] content = await GetFileContentAsync(request);

            string endpoint = ProcessEndpoint(content);

            if (string.IsNullOrEmpty(endpoint))
            {
                return new Result<List<ProcessBookResponse>>(new List<ProcessBookResponse>(), false, "The endpoint was not processed because it is malformed.");
            }

            var cacheResponses = await _cacheProvider.TryGetValue<Dictionary<string, ProcessBookResponse>>(endpoint);
            if (cacheResponses is not null)
            {
                return new Result<List<ProcessBookResponse>>(cacheResponses.Values.ToList(), true, string.Empty);
            }

            var serverResponses = await _httpClient.GetFromJsonAsync<Dictionary<string, ProcessBookResponse>>(endpoint, cancellationToken);
            if (serverResponses is null)
            {
                return new Result<List<ProcessBookResponse>>(new List<ProcessBookResponse>(), false, "The request was not processed because an error occurred.");
            }

            var result = ProcessResult(content, serverResponses);

            _cacheProvider.TrySetValue(endpoint, result, TimeSpan.FromSeconds(60));
            return new Result<List<ProcessBookResponse>>(result, true, string.Empty);
        }

        private async Task<string[]> GetFileContentAsync(ProcessBookRequest request)
        {
            if (request.BibKeys.Length <= 0)
            {
                return Array.Empty<string>();
            }

            char[] delimitters = new char[] { '\n', ',', '\r' };

            using var reader = new StreamReader(request.BibKeys.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            return content
                .Split(delimitters)
                .Where(c => !string.IsNullOrEmpty(c))
                .ToArray();
        }

        private string ProcessEndpoint(string[] content)
        {
            if (content is null || content.Length <= 0)
            {
                return string.Empty;
            }

            var values = string.Join(',', content.Select(b => $"ISBN:{b}"));
            var query = new StringBuilder();
            query.AppendJoin('=', "bibkeys", values);

            return $"?{query}&jscmd=details&format=json";
        }

        private List<ProcessBookResponse> ProcessResult(string[] content, Dictionary<string, ProcessBookResponse> serverResponses)
        {
            var result = new List<ProcessBookResponse>();
            var duplicatedKeys = content
                        .GroupBy(x => x)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.Key)
                        .ToArray();

            foreach (var item in content)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var key = $"ISBN:{item}";
                    var response = new ProcessBookResponse
                    {
                        bib_key = key,
                        details = new Details
                        {
                            authors = serverResponses[key].details?.authors ?? Array.Empty<Author>(),
                            number_of_pages = Convert.ToInt32(serverResponses[key].details?.number_of_pages),
                            publish_date = (bool)(serverResponses[key].details?.publish_date.Contains(',')) ? serverResponses[key].details?.publish_date.Replace("," , "") : serverResponses[key].details?.publish_date ?? string.Empty,
                            subtitle = serverResponses[key].details?.subtitle ?? string.Empty,
                            title = serverResponses[key].details?.title ?? string.Empty,
                        }
                    };

                    if (duplicatedKeys.Contains(item) && !result.Any(r => r.bib_key == key))
                    {
                        response.DataType = DataRetrievalType.Cache;
                    }
                    else
                    {
                        response.DataType = DataRetrievalType.Server;
                    }

                    result.Add(response);
                }
            }
            return result;
        }
    }
}