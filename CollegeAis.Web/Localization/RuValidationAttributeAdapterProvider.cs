using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace CollegeAis.Web.Localization;

public class RuValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
{
    private readonly ValidationAttributeAdapterProvider _baseProvider = new();
    private readonly IStringLocalizer _localizer;

    public RuValidationAttributeAdapterProvider(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create(
            "ValidationMessages",
            typeof(RuValidationAttributeAdapterProvider).Assembly.GetName().Name!
        );
    }

    public IAttributeAdapter? GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer? stringLocalizer)
    {
        // Подсовываем наш локализатор с русскими строками
        return _baseProvider.GetAttributeAdapter(attribute, _localizer);
    }
}