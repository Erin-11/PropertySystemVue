using KnightFrank.AuthServer.Authorization.Requirement;

namespace KnightFrank.AuthServer.Authorization
{
    public class Policies
    {
        ///<summary>Policy to allow viewing all agent records.</summary>
        public const string ViewAllAgentsPolicy = "View All Agents";

        /////<summary>Policy to allow viewing all user records.</summary>
        //public const string ViewAllUsersPolicy = "View All Users";

        /////<summary>Policy to allow adding, removing and updating all user records.</summary>
        //public const string ManageAllUsersPolicy = "Manage All Users";

        ///// <summary>Policy to allow viewing details of all roles.</summary>
        //public const string ViewAllRolesPolicy = "View All Roles";

        ///// <summary>Policy to allow viewing details of all or specific roles (Requires roleName as parameter).</summary>
        //public const string ViewRoleByRoleNamePolicy = "View Role by RoleName";

        ///// <summary>Policy to allow adding, removing and updating all roles.</summary>
        //public const string ManageAllRolesPolicy = "Manage All Roles";

        ///// <summary>Policy to allow assigning roles the user has access to (Requires new and current roles as parameter).</summary>
        //public const string AssignAllowedRolesPolicy = "Assign Allowed Roles";

        /////<summary>Policy to allow viewing all round method records.</summary>
        //public const string ViewAllRoundMethodsPolicy = "View All Round Methods";

        /////<summary>Policy to allow adding, removing and updating all round method records.</summary>
        //public const string ManageAllRoundMethodsPolicy = "Manage All Round Methods";

        /////<summary>Policy to allow viewing all payment method records.</summary>
        //public const string ViewAllPaymentMethodsPolicy = "View All Payment Methods";

        /////<summary>Policy to allow adding, removing and updating all payment method records.</summary>
        //public const string ManageAllPaymentMethodsPolicy = "Manage All Payment Methods";

        /////<summary>Policy to allow viewing all priority group records.</summary>
        //public const string ViewAllPriorityGroupsPolicy = "View All Priority Groups";

        /////<summary>Policy to allow adding, removing and updating all priority group records.</summary>
        //public const string ManageAllPriorityGroupsPolicy = "Manage All Priority Groups";

        /////<summary>Policy to allow viewing all agency records.</summary>
        //public const string ViewAllAgenciesPolicy = "View All Agencies";

        /////<summary>Policy to allow adding, removing and updating all agency records.</summary>
        //public const string ManageAllAgenciesPolicy = "Manage All Agencies";

        /////<summary>Policy to allow viewing all solicitor records.</summary>
        //public const string ViewAllSolicitorsPolicy = "View All Solicitors";

        /////<summary>Policy to allow adding, removing and updating all solicitor records.</summary>
        //public const string ManageAllSolicitorsPolicy = "Manage All Solicitors";

        /////<summary>Policy to allow viewing all property records.</summary>
        //public const string ViewAllPropertiesPolicy = "View All Properties";

        /////<summary>Policy to allow adding, removing and updating all property records.</summary>
        //public const string ManageAllPropertiesPolicy = "Manage All Properties";

        /////<summary>Policy to allow viewing all unit consumption summary records.</summary>
        //public const string ViewUnitConsumptionSummaryPolicy = "View Unit Consumption Summary";

        /////<summary>Policy to allow viewing all unit selection summary records.</summary>
        //public const string ViewUnitSelectionSummaryPolicy = "View Unit Selection Summary";

        /////<summary>Policy to allow viewing all property summary records.</summary>
        //public const string ViewPropertySummaryPolicy = "View Property Summary";

        /////<summary>Policy to allow viewing all price list records.</summary>
        //public const string ViewPriceListPolicy = "View Price List";

        /////<summary>Policy to allow viewing all preliminary agreement records.</summary>
        //public const string ViewAllPreliminaryAgreementsPolicy = "View All Preliminary Agreements";

        /////<summary>Policy to allow adding, removing and updating all preliminary agreement records.</summary>
        //public const string ManageAllPreliminaryAgreementsPolicy = "Manage All Preliminary Agreements";

        /////<summary>Policy to allow viewing all sales registration records.</summary>
        //public const string ViewSalesRegistrationPolicy = "View Sales Registration";

        ///////<summary>Policy to allow viewing all sales priority records.</summary>
        ////public const string ViewSalesPriorityPolicy = "View Sales Priority";

        /////<summary>Policy to allow viewing all balloting records.</summary>
        //public const string ViewBallotingPolicy = "View Ballotings";

        /////<summary>Policy to allow adding, removing and updating all balloting records.</summary>
        //public const string ManageAllBallotingsPolicy = "Manage All Ballotings";

        /////<summary>Policy to allow viewing all application records.</summary>
        //public const string ViewAllApplicationsPolicy = "View All Applications";

        /////<summary>Policy to allow adding, removing and updating all application records.</summary>
        //public const string ManageAllApplicationsPolicy = "Manage All Applications";

        /////<summary>Policy to allow viewing all batch records.</summary>
        //public const string ViewAllBatchesPolicy = "View All Batches";

        /////<summary>Policy to allow adding, removing and updating all batch records.</summary>
        //public const string ManageAllBatchesPolicy = "Manage All Batches";
    }

    /// <summary>
    /// Operation Policy to allow adding, viewing, updating and deleting general or specific user records.
    /// </summary>
    public static class AccountManagementOperations
    {
        public const string CreateOperationName = "Create";
        public const string ReadOperationName = "Read";
        public const string UpdateOperationName = "Update";
        public const string DeleteOperationName = "Delete";

        public static UserAccountAuthorizationRequirement Create = new(CreateOperationName);
        public static UserAccountAuthorizationRequirement Read = new(ReadOperationName);
        public static UserAccountAuthorizationRequirement Update = new(UpdateOperationName);
        public static UserAccountAuthorizationRequirement Delete = new(DeleteOperationName);
    }
}
