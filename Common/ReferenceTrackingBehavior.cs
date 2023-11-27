using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Description;

namespace Common
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ReferenceTrackingBehavior : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            // No implementation necessary
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            // No implementation necessary
            ApplyIsReference(operationDescription.Messages[0].Body.Parts[0].Type); // Parameter type
            ApplyIsReference(operationDescription.Messages[1].Body.ReturnValue.Type); // Return type
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            // Apply the IsReference property to the data contract of the operation parameter or return type
            ApplyIsReference(operationDescription.Messages[0].Body.Parts[0].Type); // Parameter type
            ApplyIsReference(operationDescription.Messages[1].Body.ReturnValue.Type); // Return type
        }

        public void Validate(OperationDescription operationDescription)
        {
            // No implementation necessary
        }

        private void ApplyIsReference(Type type)
        {
            // Get the DataContractAttribute of the type
            var dataContractAttribute = type.GetCustomAttribute<DataContractAttribute>();
            if (dataContractAttribute != null)
            {
                // Set the IsReference property to true
                dataContractAttribute.IsReference = true;
            }
        }
    }
}