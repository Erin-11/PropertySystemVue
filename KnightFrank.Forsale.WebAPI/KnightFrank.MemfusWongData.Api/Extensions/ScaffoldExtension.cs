using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KnightFrank.MemfusWongData.Api.Extensions
{
    public class CustomDbContextGenerator : CSharpDbContextGenerator
    {
        public CustomDbContextGenerator(IProviderConfigurationCodeGenerator providerConfigurationCodeGenerator, IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper)
            : base(providerConfigurationCodeGenerator, annotationCodeGenerator, cSharpHelper) { }

        public override string WriteCode(IModel model, string contextName, string connectionString, string contextNamespace, string modelNamespace, bool useDataAnnotations, bool suppressConnectionStringWarning, bool suppressOnConfiguring)
        {
            string code = base.WriteCode(model, contextName, connectionString, contextNamespace, modelNamespace, useDataAnnotations, suppressConnectionStringWarning, suppressOnConfiguring);

            var oldUsingString = "using System;" +
                                 Environment.NewLine +
                                 "using Microsoft.EntityFrameworkCore;" +
                                 Environment.NewLine +
                                 "using Microsoft.EntityFrameworkCore.Metadata;" +
                                 Environment.NewLine +
                                 "using KnightFrank.DAL.Entities.Models.MemfusWongData;";
            var newUsingString = "using KnightFrank.DAL.Entities.Models.MemfusWongData;" +
                                 Environment.NewLine +
                                 "using KnightFrank.DataAccessLayer.EF.Core;" +
                                 Environment.NewLine +
                                 "using Microsoft.EntityFrameworkCore;";
            if (code.Contains(oldUsingString))
            {
                code = code.Replace(oldUsingString, newUsingString);
            }

            string oldContextString = "public partial class " + contextName + " : DbContext";
            string newContextString = "public partial class " + contextName + " : DataContext";
            if (code.Contains(oldContextString))
            {
                code = code.Replace(oldContextString, newContextString);
            }

            return code;
        }
    }

    public class CustomeEntityTypeGenerator : CSharpEntityTypeGenerator
    {
        public CustomeEntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper)
            : base(annotationCodeGenerator, cSharpHelper) { }

        public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
        {
            string code = base.WriteCode(entityType, @namespace, useDataAnnotations);

            var oldUsingString = "using System;";
            var newUsingString = "using KnightFrank.DAL.Entities.Base;" + Environment.NewLine + "using System;";
            if (code.Contains(oldUsingString))
            {
                code = code.Replace(oldUsingString, newUsingString);
            }

            var oldEntityString = "public partial class " + entityType.Name;
            var newEntityString = "public partial class " + entityType.Name + " : ModelEntityBase";
            if (code.Contains(oldEntityString))
            {
                code = code.Replace(oldEntityString, newEntityString);
            }

            string oldIsActivePropertyString = "public string IsActive { get; set; }";
            string newIsActivePropertyString = "public override string IsActive { get; set; }";
            if (code.Contains(oldIsActivePropertyString))
            {
                code = code.Replace(oldIsActivePropertyString, newIsActivePropertyString);
            }

            string oldCreatedByPropertyString = "public string CreatedBy { get; set; }";
            string newCreatedByPropertyString = "public override string CreatedBy { get; set; }";
            if (code.Contains(oldCreatedByPropertyString))
            {
                code = code.Replace(oldCreatedByPropertyString, newCreatedByPropertyString);
            }

            string oldCreatedDatePropertyString = "public DateTime? CreatedDate { get; set; }";
            string newCreatedDatePropertyString = "public override DateTime? CreatedDate { get; set; }";
            if (code.Contains(oldCreatedDatePropertyString))
            {
                code = code.Replace(oldCreatedDatePropertyString, newCreatedDatePropertyString);
            }

            string oldModifiedByPropertyString = "public string ModifiedBy { get; set; }";
            string newModifiedByPropertyString = "public override string ModifiedBy { get; set; }";
            if (code.Contains(oldModifiedByPropertyString))
            {
                code = code.Replace(oldModifiedByPropertyString, newModifiedByPropertyString);
            }

            string oldModifiedDatePropertyString = "public DateTime? ModifiedDate { get; set; }";
            string newModifiedDatePropertyString = "public override DateTime? ModifiedDate { get; set; }";
            if (code.Contains(oldModifiedDatePropertyString))
            {
                code = code.Replace(oldModifiedDatePropertyString, newModifiedDatePropertyString);
            }

            return code;
        }
    }

    public class CustomEFDesignTimeServices : IDesignTimeServices
    {
        //Used for scaffolding database to code
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            services.AddSingleton<ICSharpEntityTypeGenerator, CustomeEntityTypeGenerator>();
            services.AddSingleton<ICSharpDbContextGenerator, CustomDbContextGenerator>();
        }
    }

    public static class ServiceCollectionExtension
    {
        public static void ConfigureDesignTime(this IServiceCollection services)
        {
            new CustomEFDesignTimeServices().ConfigureDesignTimeServices(services);
        }
    }
}
