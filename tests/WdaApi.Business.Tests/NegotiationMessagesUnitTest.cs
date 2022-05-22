using FluentValidation.TestHelper;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests;

public class NegotiationMessagesUnitTest
{
    private NegotiationMessagesValidator validator;
    public NegotiationMessagesUnitTest()
    {
        validator = new NegotiationMessagesValidator();
    }

    [Fact(DisplayName = "Empty Message error")]
    public void CreateChatMessages_WithValidParameters_EmptyConversations()
    {
        var model = new NegotiationMessages { Message = "" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(m => m.Message);
    }

    [Fact(DisplayName = "Long message error")]
    public void CreateChatMessages_WithValidParameters_LongConversations()
    {
        var model = new NegotiationMessages { Message = "mensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmmmmmmm" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(m => m.Message);
    }
}
