using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore;

 
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
}
 
public enum LanguageLevel { A1, A2, B1, B2, C1, C2 }
 
[ComplexType]
public class LanguageDetails
{
    public DateOnly TrainingStartDate { get; set; }
    public DateOnly? TrainingEndDate { get; set; }
    public LanguageLevel LanguageLevel { get; set; }
}
 
[ComplexType]
public class Language
{
    public  Languages LanguagesId { get; set; }
    public  Languages Languages { get; set; }
    public required LanguageDetails LanguageDetails { get; set; }
}

public class Languages
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
}