namespace Game.Kernel
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Net;
    using System.Text;
    using System.Web.Services.Description;
    using System.Xml.Serialization;

    public sealed class WebServiceFactory
    {
        private WebServiceFactory()
        {
        }

        private static string GetWsClassName(string wsUrl)
        {
            string[] strArray = wsUrl.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].Split(new char[] { '.' })[0];
        }

        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(url, null, methodname, args);
        }

        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            object obj3;
            string name = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = GetWsClassName(url);
            }
            try
            {
                WebClient client = new WebClient();
                ServiceDescription serviceDescription = ServiceDescription.Read(client.OpenRead(url + "?WSDL"));
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter {
                    ProtocolName = "Soap",
                    Style = ServiceDescriptionImportStyle.Client,
                    CodeGenerationOptions = CodeGenerationOptions.GenerateNewAsync | CodeGenerationOptions.GenerateProperties
                };
                importer.AddServiceDescription(serviceDescription, "", "");
                CodeNamespace namespace2 = new CodeNamespace(name);
                CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
                codeCompileUnit.Namespaces.Add(namespace2);
                importer.Import(namespace2, codeCompileUnit);
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters options = new CompilerParameters {
                    GenerateExecutable = false,
                    GenerateInMemory = true
                };
                options.ReferencedAssemblies.Add("System.dll");
                options.ReferencedAssemblies.Add("System.XML.dll");
                options.ReferencedAssemblies.Add("System.Web.Services.dll");
                options.ReferencedAssemblies.Add("System.Data.dll");
                CompilerResults results = provider.CompileAssemblyFromDom(options, new CodeCompileUnit[] { codeCompileUnit });
                if (results.Errors.HasErrors)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (CompilerError error in results.Errors)
                    {
                        builder.Append(error.ToString());
                        builder.Append(Environment.NewLine);
                    }
                    throw new Exception(builder.ToString());
                }
                Type type = results.CompiledAssembly.GetType(name + "." + classname, true, true);
                object obj2 = Activator.CreateInstance(type);
                obj3 = type.GetMethod(methodname).Invoke(obj2, args);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message, new Exception(exception.InnerException.StackTrace));
            }
            return obj3;
        }
    }
}

