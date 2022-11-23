using Akov.DataGenerator.Attributes;
using Akov.DataGenerator.Constants;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;

/// <summary>
/// Represents the data generation process for the Address response model.
/// </summary>
public class DgAddress
{
    [DgSource(ResourceType.Companies, true)]
    [DgFailure(NullProbability = 0.1)]
    public string? Company { get; set; }
        
    [DgGenerator(TemplateType.Phone)]
    [DgPattern("+45 ## ## ## ##;+420 ### ### ###")]
    [DgFailure(NullProbability = 0.05)]
    public string? Phone { get; set; }
    
    [DgGenerator(TemplateType.Email)]
    [DgFailure(NullProbability = 0.1)]
    public string? Email { get; set; }
    
    [DgSource(ResourceType.Addresses, true)]
    [DgFailure(NullProbability = 0.2)]
    public string? AddressLine { get; set; } 
    
    [DgSource(ResourceType.Cities, true)]
    [DgFailure(NullProbability = 0.1)]
    public string? City { get; set; } 
    
    [DgSource(ResourceType.Countries, true)]
    [DgFailure(NullProbability = 0.15)]
    public string? Country { get; set; } 
}