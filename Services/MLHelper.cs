using YatraBackend.Database.Models;

namespace YatraBackend.Services;

public static class MLHelper
{
    
    // Filter out words from the description if they are similar to any of the keywords
    public static List<string> FilterKeywords(string description, List<string> keywords)
    {
        // Split the description into words
        string[] words = description.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        List<string> result = []; 

        foreach (var keyword in keywords)
        {
            var splittedKeyword = keyword.Split();

            foreach (var item in splittedKeyword)
            {
                if (words.Contains(item, StringComparer.OrdinalIgnoreCase) 
                    && !result.Contains(item, StringComparer.OrdinalIgnoreCase))
                {
                    result.Add(item);
                }
            }
        }
        return result.ToList();
    }


    public static List<RecommendationResult> Recommend(Metadata query, List<Metadata> metadata, double threshold=0.5)
    {
        var pipeline = new List<Metadata>();
        
        pipeline.Add(query);
        pipeline.AddRange(metadata);
        
        var targetMetadataIndex = 0; 
        var recommendationResult = GetTopNSimilarMovies(targetMetadataIndex, pipeline, 4);

        return recommendationResult.Where(x => x.Score >= threshold).ToList();
    }
    
    
    static List<RecommendationResult> GetTopNSimilarMovies(int targetMovieIndex, List<Metadata> metadata, int n)
    {
        List<RecommendationResult> result = new();

        var targetKeywords = metadata[targetMovieIndex].Content;
        foreach (var data in metadata)
        {
            if (data != metadata[targetMovieIndex])
            {
                double similarity = CalculateCosineSimilarity(targetKeywords, data.Content);
                
                result.Add(new RecommendationResult(data.Id, similarity));
            }
        }
        result = result.OrderByDescending(m => m.Score).Take(n).ToList();

        return result;
    }
    static double CalculateCosineSimilarity(List<string> keywordsA, List<string> keywordsB)
    {
        // Convert keywords to vectors
        var vectorA = keywordsA.Select(k => keywordsB.Contains(k) ? 1 : 0).ToArray();
        var vectorB = keywordsB.Select(k => keywordsA.Contains(k) ? 1 : 0).ToArray();

        // Calculate dot product 
        double dotProduct = DotProduct(vectorA, vectorB);

        // Calculate magnitudes
        double magnitudeA = Math.Sqrt(DotProduct(vectorA, vectorA));
        double magnitudeB = Math.Sqrt(DotProduct(vectorB, vectorB));

        if (magnitudeA == 0 || magnitudeB == 0)
        {
            return 0; // Avoid division by zero
        }

        // Calculate cosine similarity
        return dotProduct / (magnitudeA * magnitudeB);
    }
    
    static double DotProduct(int[] vectorA, int[] vectorB)
    {
        double dotProduct = 0;
        int minLength = Math.Min(vectorA.Length, vectorB.Length);
        
        for (int i = 0; i < minLength; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
        }
        return dotProduct;
    }
}

public class RecommendationResult
{
    public RecommendationResult(Guid id, double score)
    {
        Id = id;
        Score = score;
    }
    public Guid Id { get; set; }
    public double Score { get; set; }
}