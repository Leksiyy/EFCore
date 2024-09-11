using BookStore.Models;

namespace BookStore.Interfaces;

public interface IReview
{
    Task<IEnumerable<Review>> GetAllReviewsAsync(int bookId);
    Task<Review> GetReviewAsync(int id);

    Task AddReviewAsync(Review review);
    Task DeleteReviewAsync(Review review);
}
