namespace ReportingService.Services;

public sealed class ScreenValidationService
{
    public void ValidateScreen(string screenId, IDictionary<string, string> state, IDictionary<string, string> errors)
    {
        errors.Clear();
        if (!UiScreenRegistry.Screens.TryGetValue(screenId, out var descriptor))
        {
            errors["screen"] = "Unknown screen metadata.";
            return;
        }

        foreach (var field in descriptor.Fields.Values.OrderBy(field => field.TabOrder))
        {
            if (!field.Submittable)
            {
                continue;
            }
            var stateKey = string.IsNullOrWhiteSpace(field.BindingName) ? field.FieldId : field.BindingName;
            var value = state.TryGetValue(stateKey, out var fieldValue)
                ? (fieldValue ?? string.Empty).Trim()
                : string.Empty;
            state[stateKey] = value;

            if (field.Required && string.IsNullOrWhiteSpace(value))
            {
                errors[field.FieldId] = $"{field.Label} is required.";
                continue;
            }

            if (field.MaxLength is int maxLength && value.Length > maxLength)
            {
                errors[field.FieldId] = $"{field.Label} must be {maxLength} characters or fewer.";
                continue;
            }

            if (!string.IsNullOrWhiteSpace(value)
                && field.Rules.Any(rule => rule.Kind == "numeric")
                && !value.All(char.IsDigit))
            {
                errors[field.FieldId] = $"{field.Label} must be numeric.";
            }
        }
    }
}
