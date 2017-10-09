using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer
{
    public class StringDataMamber
    {
        public string DataText { get; set; }
    }

    public class ViolationDetail
    {
        public int ViolationDetailID { get; set; }
        public string Type_Of_Vehicle { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Type_Of_Violations { get; set; }
        public int DocumentTypeID { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public int? CountForFaxId { get; set; }
    }
    public class DriverConviction
    {
        public int ConvictionDetailsId { get; set; }
        public string DateOfConviction { get; set; }
        public string Offense { get; set; }
        public string Location { get; set; }
        public string TypeOfVehicleOperated { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public int? CountForFaxId { get; set; }
    }

    public class DynamicControlResponse
    {
        public int DynamicControlID { get; set; }
        public int DocumentTypeID { get; set; }
        public string labelName { get; set; }
        public string ControlName { get; set; }
        public string ControlType { get; set; }
        public string DynamicControlValueText { get; set; }
        public Nullable<int> DyanamicControlValueID { get; set; }
        public string DropDownValue { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
    public class DynamicControlResponseList : List<DynamicControlResponse>
    {

    }

    public class DynamicControlWithValueResponse
    {
        public int DynamicControlValudId { get; set; }
        public string DynamicControlValueText { get; set; }
        public string ButtonId { get; set; }
    }

    public class DynamicControlWithControlTypeResponse : DynamicControlResponse
    {
        public string ControlTypeId { get; set; }
        public string DocumentTypeName { get; set; }
    }

    public class DropDownValue
    {
        public string Text { get; set; }
        public int DyanamicControlID { get; set; }
        public int DynamicControlValueId { get; set; }
    }

    public class UpdateControlClass
    {
        public string Text { get; set; }
        public int DyanamicControlID { get; set; }
        public int DynamicControlValueId { get; set; }
    }

    public class DocumentTypeDetails
    {
        public int? DocumentTypeId { get; set; }
        public int? CountForFaxId { get; set; }

    }

    public class DocumentTypeReviewList
    {
        public int? DocumentTypeId { get; set; }
        public bool IsReview = false;
        public int? CountForFaxId { get; set; }

    }

    public class DropDownValueTextTable
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class DynamicControlValueNew
    {
        public int DyanamicControlValueID { get; set; }
        public int TempCPScreenDataID { get; set; }
        public int DynamicControlID { get; set; }
        public string DynamicControlValueText { get; set; }
        public int UserId { get; set; }
        public Nullable<int> RoboActivitiesID { get; set; }
        public int CountForFaxId { get; set; }




    }

    public class UserDetails
    {
        public int UserDetailsID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int FADVUserID { get; set; }

        public int RoleId { get; set; }
        public string Password { get; set; }

        public int LoginAttempt { get; set; }

        public string LocktheAccount { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }
        public string EmailId { get; set; }
        public string IsActive { get; set; }





    }


    public class DynamicControl
    {
        public int DynamicControlID { get; set; }
        public int DocumentTypeID { get; set; }
        public string labelName { get; set; }
        public string ControlName { get; set; }
        public string ControlType { get; set; }
        public string DropDownValue { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public string IsActive { get; set; }
        public string AutomationVarName { get; set; }
    }

    public class DocumentTypeEntryDetailNew
    {
        public int DocumentTypeEntryId { get; set; }
        public int TempCPScreenDataID { get; set; }
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int CountForFaxId { get; set; }

    }

    public class TempTaskAssignmentNew
    {
        public int TempTaskAssignmentID { get; set; }
        public int FaxID { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }
        public int TempCPScreenDataID { get; set; }
        public int ProcessStatus { get; set; }
        public string IsProcessed { get; set; }
        public int NoOfPagesCompleted { get; set; }


    }


    public class CMS_CPScreenData_Temp
    { 
        public int TempCPScreenDataID { get; set; }
        public int FaxID { get; set; }
        public DateTime ReceiveDate { get; set; }

        public Nullable <int> CustomerID { get; set; }
        public string SourceFile { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int NoOfDataEntries { get; set; }
        public bool IsDataEntriesInProgress { get; set; }
        public DateTime DateofCreation { get; set; }
        public DateTime DateOfModification { get; set; }
        public string Comment { get; set; }
        public int CountAssignedUser { get; set; }
        public int RoboActivitiesID { get; set; }



    }

    public class LocationJoinCustomerDetail
    {

        public string Fadv_LocationID { get; set; }
        public string LocationName { get; set; }


    }

    public class TaskOperationNew
    {
        public int TaskOperationId { get; set; }
        public int FaxId { get; set; }
        public int UserId { get; set; }

        public int CustomerID { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }


    }

    public partial class uspGetAllEnteredPageNo_ResultNew
    {
        public int DocumentTypeID { get; set; }
        public string ControlName { get; set; }
        public int DyanamicControlValueID { get; set; }
        public string DynamicControlValueText { get; set; }
        public Nullable<int> CountForFaxId { get; set; }
    }




}
