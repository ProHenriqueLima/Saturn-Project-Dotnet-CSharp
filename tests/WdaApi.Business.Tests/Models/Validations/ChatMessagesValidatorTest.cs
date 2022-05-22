using FluentValidation.TestHelper;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests.Models.Validations;

public class ChatMessagesValidatorTest
{
    private ChatMessagesValidator validator;
    public ChatMessagesValidatorTest()
    {
        validator = new ChatMessagesValidator();
    }

    [Fact(DisplayName = "Empty Message error")]
    public void CreateChatMessages_WithValidParameters_EmptyConversations()
    {
        var model = new ChatMessages { Message = "" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(m => m.Message);
    }

    [Fact(DisplayName = "Long Message error")]
    public void CreateChatMessages_WithValidParameters_LongConversations()
    {
        var model = new ChatMessages { Message = "mensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmensagemmmmmmmmmmmmmmmm" };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(m => m.Message);
    }
}