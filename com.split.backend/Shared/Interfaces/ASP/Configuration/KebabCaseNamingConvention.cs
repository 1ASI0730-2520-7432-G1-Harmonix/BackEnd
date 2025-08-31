using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace com.split.backend.Shared.Interfaces.ASP.Configuration;

public class KebabCaseNamingConvention : IControllerModelConvention
{
    /// <summary>
    ///  This method applies the kebab-case naming convention to the controller
    /// </summary>
    /// <param name="controller">The <see cref="ControllerModel"/>></param>
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);

        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
        {
            
        }
    }
    
}