using FluentValidation.TestHelper;
using WdaApi.Business.Models;
using WdaApi.Business.Models.Validations;
using Xunit;

namespace WdaApi.Business.Tests
{
    public class VacancyUnitTest
    {
        private VacancyValidator validator;
        public VacancyUnitTest()
        {
            validator = new VacancyValidator();
        }

        [Fact(DisplayName = "Invalid Price value")]
        public void CreateVacancy_WithValidParameters_InvalidPriceValue()
        {
            var model = new Vacancy { Price = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact(DisplayName = "Invalid Weight value")]
        public void CreateVacancy_WithValidParameters_InvalidWeightValue()
        {
            var model = new Vacancy { Weight = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact(DisplayName = "Invalid Step value")]
        public void CreateVacancy_WithValidParameters_InvalidStepValue()
        {
            var model = new Vacancy { Step = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Step);
        }
    }
}
