using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KnightFrank.AuthServer.Authorization.Common
{
    public static class ApplicationPermissions
    {
        public static ReadOnlyCollection<ApplicationPermission> AllPermissions;

        public const string AgentGroupName = "Agent Permissions";
        public static ApplicationPermission viewAgents = new("View Agent", "agent.view", AgentGroupName, "Permission to view agent details");

        //public const string SystemConfigsPermissionGroupName = "System Config Permissions";
        //public static ApplicationPermission ViewSystemConfigs = new ApplicationPermission("View System Configs", "systemconfigs.view", SystemConfigsPermissionGroupName, "Permission to view other system configs details");
        //public static ApplicationPermission ManageSystemConfigs = new ApplicationPermission("Manage System Configs", "systemconfigs.manage", SystemConfigsPermissionGroupName, "Permission to create, delete and modify other system configs details");

        //public const string PublicHolidaysPermissionGroupName = "Public Holiday Permissions";
        //public static ApplicationPermission ViewPublicHolidays = new ApplicationPermission("View Public Holidays", "publicHolidays.view", PublicHolidaysPermissionGroupName, "Permission to view other public holidays details");
        //public static ApplicationPermission ManagePublicHolidays = new ApplicationPermission("Manage Public Holidays", "publicHolidays.manage", PublicHolidaysPermissionGroupName, "Permission to create, delete and modify other public holidays details");

        //public const string DistrictsPermissionGroupName = "District Permissions";
        //public static ApplicationPermission ViewDistricts = new ApplicationPermission("View Districts", "districts.view", DistrictsPermissionGroupName, "Permission to view other districts details");
        //public static ApplicationPermission ManageDistricts = new ApplicationPermission("Manage Districts", "districts.manage", DistrictsPermissionGroupName, "Permission to create, delete and modify other districts details");

        //public const string BanksPermissionGroupName = "Bank Permissions";
        //public static ApplicationPermission ViewBanks = new ApplicationPermission("View Banks", "banks.view", BanksPermissionGroupName, "Permission to view other banks details");
        //public static ApplicationPermission ManageBanks = new ApplicationPermission("Manage Banks", "banks.manage", BanksPermissionGroupName, "Permission to create, delete and modify other banks details");

        public const string RolesPermissionGroupName = "Role Permissions";
        public static ApplicationPermission ViewRoles = new ApplicationPermission("View Roles", "roles.view", RolesPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageRoles = new ApplicationPermission("Manage Roles", "roles.manage", RolesPermissionGroupName, "Permission to create, delete and modify roles");
        public static ApplicationPermission AssignRoles = new ApplicationPermission("Assign Roles", "roles.assign", RolesPermissionGroupName, "Permission to assign roles to users");

        public const string UsersPermissionGroupName = "User Permissions";
        public static ApplicationPermission ViewUsers = new ApplicationPermission("View Users", "users.view", UsersPermissionGroupName, "Permission to view other users account details");
        public static ApplicationPermission ManageUsers = new ApplicationPermission("Manage Users", "users.manage", UsersPermissionGroupName, "Permission to create, delete and modify other users account details");

        //public const string RoundMethodPermissionGroupName = "Round Method Permissions";
        //public static ApplicationPermission ViewRoundMethods = new ApplicationPermission("View Round Methods", "roundmethods.view", RoundMethodPermissionGroupName, "Permission to view round methods");
        //public static ApplicationPermission ManageRoundMethods = new ApplicationPermission("Manage Round Methods", "roundmethods.manage", RoundMethodPermissionGroupName, "Permission to create, delete and modify round methods");
        //public static ApplicationPermission CreateRoundMethods = new ApplicationPermission("Create Round Methods", "roundmethods.create", RoundMethodPermissionGroupName, "Permission to create round methods");
        //public static ApplicationPermission DeleteRoundMethods = new ApplicationPermission("Delete Round Methods", "roundmethods.delete", RoundMethodPermissionGroupName, "Permission to delete round methods");
        //public static ApplicationPermission ModifyRoundMethods = new ApplicationPermission("Modify Round Methods", "roundmethods.modify", RoundMethodPermissionGroupName, "Permission to modify round methods");
        //public static ApplicationPermission ApproveRoundMethodsSubmission = new ApplicationPermission("Round Methods Approval", "roundmethods.approval", RoundMethodPermissionGroupName, "Permission to approve round methods submission");

        //public const string PaymentMethodPermissionGroupName = "Payment Method Permissions";
        //public static ApplicationPermission ViewPaymentMethods = new ApplicationPermission("View Payment Methods", "paymentmethods.view", PaymentMethodPermissionGroupName, "Permission to view payment methods");
        //public static ApplicationPermission ManagePaymentMethods = new ApplicationPermission("Manage Payment Methods", "paymentmethods.manage", PaymentMethodPermissionGroupName, "Permission to create, delete and modify payment methods");
        //public static ApplicationPermission CreatePaymentMethods = new ApplicationPermission("Create Payment Methods", "paymentmethods.create", PaymentMethodPermissionGroupName, "Permission to create payment methods");
        //public static ApplicationPermission DeletePaymentMethods = new ApplicationPermission("Delete Payment Methods", "paymentmethods.delete", PaymentMethodPermissionGroupName, "Permission to delete payment methods");
        //public static ApplicationPermission ModifyPaymentMethods = new ApplicationPermission("Modify Payment Methods", "paymentmethods.modify", PaymentMethodPermissionGroupName, "Permission to modify payment methods");
        //public static ApplicationPermission ApprovePaymentMethodsSubmission = new ApplicationPermission("Payment Methods Approval", "paymentmethods.approval", PaymentMethodPermissionGroupName, "Permission to approve payment methods submission");

        //public const string AgencyPermissionGroupName = "Agency Permissions";
        //public static ApplicationPermission ViewAgencies = new ApplicationPermission("View Agencies", "agencies.view", AgencyPermissionGroupName, "Permission to view agencies");
        //public static ApplicationPermission ManageAgencies = new ApplicationPermission("Manage Agencies", "agencies.manage", AgencyPermissionGroupName, "Permission to create, delete and modify agencies");
        //public static ApplicationPermission CreateAgencies = new ApplicationPermission("Create Agencies", "agencies.create", AgencyPermissionGroupName, "Permission to create agencies");
        //public static ApplicationPermission DeleteAgencies = new ApplicationPermission("Delete Agencies", "agencies.delete", AgencyPermissionGroupName, "Permission to delete agencies");
        //public static ApplicationPermission ModifyAgencies = new ApplicationPermission("Modify Agencies", "agencies.modify", AgencyPermissionGroupName, "Permission to modify agencies");
        //public static ApplicationPermission ApproveAgenciesSubmission = new ApplicationPermission("Agencies Approval", "agencies.approval", AgencyPermissionGroupName, "Permission to approve agencies submission");

        //public const string SolicitorPermissionGroupName = "Solicitor Permissions";
        //public static ApplicationPermission ViewSolicitors = new ApplicationPermission("View Solicitors", "solicitors.view", SolicitorPermissionGroupName, "Permission to view solicitors");
        //public static ApplicationPermission ManageSolicitors = new ApplicationPermission("Manage Solicitors", "solicitors.manage", SolicitorPermissionGroupName, "Permission to create, delete and modify solicitors");
        //public static ApplicationPermission CreateSolicitors = new ApplicationPermission("Create Solicitors", "solicitors.create", SolicitorPermissionGroupName, "Permission to create solicitors");
        //public static ApplicationPermission DeleteSolicitors = new ApplicationPermission("Delete Solicitors", "solicitors.delete", SolicitorPermissionGroupName, "Permission to delete solicitors");
        //public static ApplicationPermission ModifySolicitors = new ApplicationPermission("Modify Solicitors", "solicitors.modify", SolicitorPermissionGroupName, "Permission to modify solicitors");
        //public static ApplicationPermission ApproveSolicitorsSubmission = new ApplicationPermission("Solicitors Approval", "solicitors.approval", SolicitorPermissionGroupName, "Permission to approve solicitors submission");

        //public const string PriorityGroupPermissionGroupName = "Priority Group Permissions";
        //public static ApplicationPermission ViewPriorityGroups = new ApplicationPermission("View Priority Groups", "prioritygroups.view", PriorityGroupPermissionGroupName, "Permission to view priority groups");
        //public static ApplicationPermission ManagePriorityGroups = new ApplicationPermission("Manage Priority Groups", "prioritygroups.manage", PriorityGroupPermissionGroupName, "Permission to create, delete and modify priority groups");
        //public static ApplicationPermission CreatePriorityGroups = new ApplicationPermission("Create Priority Groups", "prioritygroups.create", PriorityGroupPermissionGroupName, "Permission to create priority groups");
        //public static ApplicationPermission DeletePriorityGroups = new ApplicationPermission("Delete Priority Groups", "prioritygroups.delete", PriorityGroupPermissionGroupName, "Permission to delete priority groups");
        //public static ApplicationPermission ModifyPriorityGroups = new ApplicationPermission("Modify Priority Groups", "prioritygroups.modify", PriorityGroupPermissionGroupName, "Permission to modify priority groups");
        //public static ApplicationPermission ApprovePriorityGroupsSubmission = new ApplicationPermission("Priority Groups Approval", "prioritygroups.approval", PriorityGroupPermissionGroupName, "Permission to approve priority groups submission");

        //public const string PropertyPermissionGroupName = "Property Permissions";
        //public static ApplicationPermission ViewProperties = new ApplicationPermission("View Properties", "properties.view", PropertyPermissionGroupName, "Permission to view properties");
        //public static ApplicationPermission ManageProperties = new ApplicationPermission("Manage Properties", "properties.manage", PropertyPermissionGroupName, "Permission to create, delete and modify properties");
        //public static ApplicationPermission CreateProperties = new ApplicationPermission("Create Properties", "properties.create", PropertyPermissionGroupName, "Permission to create properties");
        //public static ApplicationPermission DeleteProperties = new ApplicationPermission("Delete Properties", "properties.delete", PropertyPermissionGroupName, "Permission to delete properties");
        //public static ApplicationPermission ModifyProperties = new ApplicationPermission("Modify Properties", "properties.modify", PropertyPermissionGroupName, "Permission to modify properties");
        //public static ApplicationPermission ApprovePropertyUnitSubmission = new ApplicationPermission("Property Units Approval", "propertyunits.approval", PropertyPermissionGroupName, "Permission to approve proerty units submission");

        //public const string BallotingResultGroupName = "Balloting Result Permissions";
        //public static ApplicationPermission ViewBallotingResult = new ApplicationPermission("View Balloting Result", "ballotingresult.view", BallotingResultGroupName, "Permission to view balloting result");

        //public const string RegisterOfTransactionGroupName = "Register of Transactions Permissions";
        //public static ApplicationPermission ViewRegisterOfTransaction = new ApplicationPermission("View Register of Transaction", "registeroftransaction.view", RegisterOfTransactionGroupName, "Permission to view register of transaction");

        //public const string SalesReportPermissionGroupName = "Sales Report Permissions";
        //public static ApplicationPermission ViewSalesReport = new ApplicationPermission("View Sales Report", "salesreport.view", SalesReportPermissionGroupName, "Permission to view sales report");

        //public const string ApplicationSummaryPermissionGroupName = "Application Summary Permissions";
        //public static ApplicationPermission ViewApplicationSummary = new ApplicationPermission("View Application Summary", "applicationsummary.view", ApplicationSummaryPermissionGroupName, "Permission to view application summary");

        //public const string UnitConsumptionSummaryPermissionGroupName = "Unit Consumption Summary Permissions";
        //public static ApplicationPermission ViewUnitConsumptionSummary = new ApplicationPermission("View Unit Consumption Summary", "unitconsumptionsummary.view", UnitConsumptionSummaryPermissionGroupName, "Permission to view unitconsumptionsummary");

        //public const string UnitSelectionSummaryGroupName = "Unit Selection Summary Permissions";
        //public static ApplicationPermission ViewUnitSelectionSummary = new ApplicationPermission("View Unit Selection Summary", "unitselectionsummary.view", UnitSelectionSummaryGroupName, "Permission to view unit selection summary");

        //public const string PropertySummaryGroupName = "Property Summary Permissions";
        //public static ApplicationPermission ViewPropertySummary = new ApplicationPermission("View Property Summary", "propertysummary.view", PropertySummaryGroupName, "Permission to view property summary");

        //public const string PriceListGroupName = "Price List Permissions";
        //public static ApplicationPermission ViewPriceList = new ApplicationPermission("View Price List", "pricelist.view", PriceListGroupName, "Permission to view price list");

        //public const string PreliminaryAgreementGroupName = "Preliminary Agreement Permissions";
        //public static ApplicationPermission ViewPreliminaryAgreements = new ApplicationPermission("View Preliminary Agreements", "preliminaryagreements.view", PreliminaryAgreementGroupName, "Permission to view preliminary agreements");
        //public static ApplicationPermission ManagePreliminaryAgreements = new ApplicationPermission("Manage Preliminary Agreements", "preliminaryagreements.manage", PreliminaryAgreementGroupName, "Permission to create, delete and modify preliminary agreements");
        //public static ApplicationPermission CreatePreliminaryAgreements = new ApplicationPermission("Create Preliminary Agreements", "preliminaryagreements.create", PreliminaryAgreementGroupName, "Permission to create preliminary agreements");
        //public static ApplicationPermission DeletePreliminaryAgreements = new ApplicationPermission("Delete Preliminary Agreements", "preliminaryagreements.delete", PreliminaryAgreementGroupName, "Permission to delete preliminary agreements");
        //public static ApplicationPermission ModifyPreliminaryAgreements = new ApplicationPermission("Modify Preliminary Agreements", "preliminaryagreements.modify", PreliminaryAgreementGroupName, "Permission to modify preliminary agreements");
        //public static ApplicationPermission ApprovePreliminaryAgreementSubmission = new ApplicationPermission("Preliminary Agreements Approval", "preliminaryagreements.approval", PreliminaryAgreementGroupName, "Permission to approve preliminary agreements submission");

        //public const string SalesRegistrationGroupName = "Sales Registration Permissions";
        //public static ApplicationPermission ViewSalesRegistration = new ApplicationPermission("View Sales Registration", "salesregistration.view", SalesRegistrationGroupName, "Permission to view sales registration");

        //public const string SalesPriorityGroupName = "Sales Priority Permissions";
        //public static ApplicationPermission ViewSalesPriority = new ApplicationPermission("View Sales Priority", "salespriority.view", SalesPriorityGroupName, "Permission to view sales priority");

        //public const string BallotingGroupName = "Balloting Permissions";
        //public static ApplicationPermission ViewBallotings = new ApplicationPermission("View Ballotings", "ballotings.view", BallotingGroupName, "Permission to view ballotings");
        //public static ApplicationPermission ManageBallotings = new ApplicationPermission("Manage Ballotings", "ballotings.manage", BallotingGroupName, "Permission to create, delete and modify ballotings");
        //public static ApplicationPermission CreateBallotings = new ApplicationPermission("Create Ballotings", "ballotings.create", BallotingGroupName, "Permission to create ballotings");
        //public static ApplicationPermission DeleteBallotings = new ApplicationPermission("Delete Ballotings", "ballotings.delete", BallotingGroupName, "Permission to delete ballotings");
        //public static ApplicationPermission ModifyBallotings = new ApplicationPermission("Modify Ballotings", "ballotings.modify", BallotingGroupName, "Permission to modify ballotings");

        //public const string ApplicationGroupName = "Application Permissions";
        //public static ApplicationPermission ViewApplications = new ApplicationPermission("View Applications", "applications.view", ApplicationGroupName, "Permission to view applications");
        //public static ApplicationPermission ManageApplications = new ApplicationPermission("Manage Applications", "applications.manage", ApplicationGroupName, "Permission to create, delete and modify applications");
        //public static ApplicationPermission CreateApplications = new ApplicationPermission("Create Applications", "applications.create", ApplicationGroupName, "Permission to create applications");
        //public static ApplicationPermission DeleteApplications = new ApplicationPermission("Delete Applications", "applications.delete", ApplicationGroupName, "Permission to delete applications");
        //public static ApplicationPermission ModifyApplications = new ApplicationPermission("Modify Applications", "applications.modify", ApplicationGroupName, "Permission to modify applications");

        //public const string BatchGroupName = "Batch Permissions";
        //public static ApplicationPermission ViewBatches = new ApplicationPermission("View Batches", "batches.view", BatchGroupName, "Permission to view batches");
        //public static ApplicationPermission ManageBatches = new ApplicationPermission("Manage Batches", "batches.manage", BatchGroupName, "Permission to create, delete and modify batches");
        //public static ApplicationPermission CreateBatches = new ApplicationPermission("Create Batches", "batches.create", BatchGroupName, "Permission to create batches");
        //public static ApplicationPermission DeleteBatches = new ApplicationPermission("Delete Batches", "batches.delete", BatchGroupName, "Permission to delete batches");
        //public static ApplicationPermission ModifyBatches = new ApplicationPermission("Modify Batches", "batches.modify", BatchGroupName, "Permission to modify batches");

        static ApplicationPermissions()
        {
            List<ApplicationPermission> allPermissions = new()
            {
                viewAgents,

                //ViewBatches,
                //ManageBatches,
                //CreateBatches,
                //DeleteBatches,
                //ModifyBatches,

                //ViewApplications,
                //ManageApplications,
                //CreateApplications,
                //DeleteApplications,
                //ModifyApplications,

                //ViewSalesPriority,
                //ViewBallotings,
                //CreateBallotings,
                //DeleteBallotings,
                //ModifyBallotings,

                //ViewSalesRegistration,

                //ViewPreliminaryAgreements,
                //ManagePreliminaryAgreements,
                //CreatePreliminaryAgreements,
                //DeletePreliminaryAgreements,
                //ModifyPreliminaryAgreements,
                //ApprovePreliminaryAgreementSubmission,

                //ViewPriceList,
                //ViewPropertySummary,
                //ViewUnitSelectionSummary,
                //ViewUnitConsumptionSummary,
                //ViewApplicationSummary,
                //ViewSalesReport,
                //ViewRegisterOfTransaction,
                //ViewBallotingResult,

                //ViewProperties,
                //ManageProperties,
                //CreateProperties,
                //DeleteProperties,
                //ModifyProperties,
                //ApprovePropertyUnitSubmission,

                //ViewPriorityGroups,
                //ManagePriorityGroups,
                //CreatePriorityGroups,
                //DeletePriorityGroups,
                //ModifyPriorityGroups,
                //ApprovePriorityGroupsSubmission,

                //ViewSolicitors,
                //ManageSolicitors,
                //CreateSolicitors,
                //DeleteSolicitors,
                //ModifySolicitors,
                //ApproveSolicitorsSubmission,

                //ViewAgencies,
                //ManageAgencies,
                //CreateAgencies,
                //DeleteAgencies,
                //ModifyAgencies,
                //ApproveAgenciesSubmission,

                //ViewPaymentMethods,
                //ManagePaymentMethods,
                //CreatePaymentMethods,
                //DeletePaymentMethods,
                //ModifyPaymentMethods,
                //ApprovePaymentMethodsSubmission,

                //ViewRoundMethods,
                //ManageRoundMethods,
                //CreateRoundMethods,
                //DeleteRoundMethods,
                //ModifyRoundMethods,
                //ApproveRoundMethodsSubmission,

                //ViewUsers,
                //ManageUsers,

                //ViewRoles,
                //ManageRoles,
                //AssignRoles,

                //ViewBanks,
                //ManageBanks,

                //ViewDistricts,
                //ManageDistricts,

                //ViewPublicHolidays,
                //ManagePublicHolidays,

                //ViewSystemConfigs,
                //ManageSystemConfigs
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static ApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).SingleOrDefault();
        }

        public static ApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).SingleOrDefault();
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        //public static string[] GetAdministrativePermissionValues()
        //{
        //    return new string[] { ManageUsers, ManageRoles, AssignRoles };
        //}
    }

    public class ApplicationPermission
    {
        public ApplicationPermission()
        { }

        public ApplicationPermission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(ApplicationPermission permission)
        {
            return permission.Value;
        }
    }
}
