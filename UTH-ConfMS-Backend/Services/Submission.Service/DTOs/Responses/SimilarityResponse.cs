namespace Submission.Service.DTOs.Responses;

/// <summary>
/// Response for text similarity check
/// </summary>
public class SimilarityResponse
{
    /// <summary>
    /// Overall similarity score (0-100%)
    /// </summary>
    public double SimilarityScore { get; set; }
    
    /// <summary>
    /// Cosine similarity score (0-100%)
    /// </summary>
    public double CosineSimilarity { get; set; }
    
    /// <summary>
    /// Jaccard similarity score (0-100%)
    /// </summary>
    public double JaccardSimilarity { get; set; }
    
    /// <summary>
    /// Whether the content is considered plagiarized (>70% similarity)
    /// </summary>
    public bool IsPlagiarized { get; set; }
    
    /// <summary>
    /// Human-readable message about similarity level
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
