using FluentValidation.TestHelper;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests;

public class NegotiationUnitTest
{
    private NegotiationValidator validator;
    public NegotiationUnitTest()
    {
        validator = new NegotiationValidator();
    }

    [Fact(DisplayName = "Validate trade status")]
    public void CreateChatMessages_WithValidParameters_EmptyNegotiations()
    {
        var model = new Negotiation { StatusNegotiation = true };
        var result = validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(m => m.StatusNegotiation);
    }

    [Fact(DisplayName = "Short Price value error")]
    public void CreateChatMessages_WithValidParameters_LessNegotiations()
    {
        var model = new Negotiation { Price = -1 };
        var result = validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(m => m.Price);
    }

}