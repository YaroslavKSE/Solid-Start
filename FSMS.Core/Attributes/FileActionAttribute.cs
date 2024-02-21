namespace FSMS.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class FileActionAttribute : Attribute
{
    public string ActionName { get; }
    public string SupportedExtension { get; }

    public FileActionAttribute(string actionName, string supportedExtension)
    {
        ActionName = actionName;
        SupportedExtension = supportedExtension;
    }
}