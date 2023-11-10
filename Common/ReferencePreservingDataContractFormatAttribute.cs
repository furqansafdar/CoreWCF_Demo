using System;
using System.Collections.Generic;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using System.Runtime.Serialization;
using System.Xml;

namespace Common
{
    //https://blogs.msdn.microsoft.com/sowmy/2006/03/26/preserving-object-reference-in-wcf/
    public class ReferencePreservingDataContractFormatAttribute : Attribute, IOperationBehavior
    {
        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
        {
            IOperationBehavior innerBehavior = new ReferencePreservingDataContractSerializerOperationBehavior(description);
            innerBehavior.ApplyClientBehavior(description, proxy);
        }

        public void ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
        {
            IOperationBehavior innerBehavior = new ReferencePreservingDataContractSerializerOperationBehavior(description);
            innerBehavior.ApplyDispatchBehavior(description, dispatch);
        }

        public void Validate(OperationDescription description)
        {
        }

        #endregion
    }

    class ReferencePreservingDataContractSerializerOperationBehavior : DataContractSerializerOperationBehavior
    {
        public ReferencePreservingDataContractSerializerOperationBehavior(OperationDescription operationDescription)
            : base(operationDescription) { }

        public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
        {
            //return new DataContractSerializer(type, name, ns, knownTypes,
            //    0x7FFF /*maxItemsInObjectGraph*/,
            //    false /*ignoreExtensionDataObject*/,
            //    true /*preserveObjectReferences*/,
            //    null /*dataContractSurrogate*/);

            var settings = new DataContractSerializerSettings
            {
                RootName = name,
                RootNamespace = ns,
                KnownTypes = knownTypes,
                MaxItemsInObjectGraph = 0x7FFF,
                IgnoreExtensionDataObject = false,
                PreserveObjectReferences = true,
                DataContractResolver = null
            };

            return new DataContractSerializer(type, settings);
        }
    }
}
