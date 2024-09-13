using System.Security.Cryptography;
using EFCore;
using Final.Models;
using Microsoft.EntityFrameworkCore;
using Type = Final.Models.Type;

namespace Final;

public class QueryHandler
{
    static void AddMoney(User user, decimal money)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var sql = "UPDATE Users SET Balance = Balance + @p0 WHERE Id = @p1";

            db.Database.ExecuteSqlRaw(sql, money, user.Id);
        }
    }
    
    static void AddTrancsaction(Transaction transaction)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            if (transaction.Type == Type.Income)
            {
                transaction.User.Balance += transaction.Amount;
            }
            else
            {
                transaction.User.Balance -= transaction.Amount;
            }
            
            if(transaction.User.Balance <= 0) throw new Exception("Insufficient Balance");
            
            db.Transactions.Add(transaction);
            
            db.SaveChanges();
        }
    }

    static List<Transaction> GetTrancsactionList(User user)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            List<Transaction> trancsactions = db.Transactions.ToList();
            return trancsactions;
        }
    }

    static List<decimal> GetTrancsactionAmountList(User user)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            decimal result1 = db.Users.FirstOrDefault(e => e.Equals(user)).Transactions.Where(e => e.Type == Type.Income).Sum(t => t.Amount);
            decimal result2 = db.Users.FirstOrDefault(e => e.Equals(user)).Transactions.Where(e => e.Type == Type.Consumption).Sum(t => t.Amount);
            return [result1, result2];
        }
    }

    static List<Transaction> GetFilteredTransactionList(User user, DateTime startDate, DateTime endDate)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var transactions = db.Transactions
                .Where(e => e.User.Equals(user) && e.Date >= startDate && e.Date <= endDate)
                .ToList();

            return transactions;
        }
    }

    static ICollection<Category>? GetReport(Transaction transaction)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Transactions
                .Where(t => t.UserId == transaction.User.Id && t.Type == Type.Consumption && t.Amount > 0)
                .GroupBy(t => t.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderByDescending(ct => ct.TotalAmount)
                .FirstOrDefault()
                ?.Category;
        }
    }
}