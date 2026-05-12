namespace Customer and Account.Models;

public sealed record UiValidationRule(string FieldId, IReadOnlyList<string> RuleIds);
public sealed record UiEventBinding(string EventId, string Trigger, int StepCount);
