using FluentValidation.TestHelper;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests
{
    public class RoadMapUnitTest
    {
        private RoadMapValidator validator;
        public RoadMapUnitTest()
        {
            validator = new RoadMapValidator();
        }

        [Fact(DisplayName = "Invalid RoadMap name")]
        public void CreateRoadMap_WithValidParameters_InvalidName()
        {
            var model = new RoadMap { Name = "" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact(DisplayName = "Invalid RoadMap long name")]
        public void CreateRoadMap_WithValidParameters_LongName()
        {
            var model = new RoadMap { Name = "Fortalezaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact(DisplayName = "Invalid short Status")]
        public void CreateRoadMap_WithValidParameters_InvalidShortStatus()
        {
            var model = new RoadMap { StatusId = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.StatusId);
        }

        [Fact(DisplayName = "Invalid long Status")]
        public void CreateRoadMap_WithValidParameters_InvalidLongStatus()
        {
            var model = new RoadMap { StatusId = 4 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.StatusId);
        }
    }
}
