using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiddleLayer
{
    public class UserControlClass
    {
    }

    public class DriverVilocation
    {
        public int VilocationDetailsId { get; set; }
        public string Typeofviolations { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string TypeofVehicle { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public int? CountForFaxId { get; set; }
    }

    public class DriverLicence
    {
        public int DriverLicenceId { get; set; }
        public string Restriction { get; set; }
        public string Endorsement { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }

    }

    public class PreviousEmployeement
    {
        public int PreviousEmploymentDetailId { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string EmployerName { get; set; }
        public string EmploymentStartDate { get; set; }
        public string EmploymentEndDate { get; set; }
    }

    public class PreviousEmployer
    {
        public int PreviousEmployerDetailId { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string PreviousEmployerName { get; set; }
        public string PreviousEmployerStreetAddress { get; set; }
        public string PreviousEmployerCity { get; set; }
        public string PreviousEmployerState { get; set; }
        public string PreviousEmployerZipcode { get; set; }
        public string EmploymentStartDate { get; set; }
        public string EmploymentEndDate { get; set; }
        public string ReasonForLeavingPreviousEmployments { get; set; }

    }

    public class CEDPreviousEmployer
    {
        public int PreviousEmployerDetailId { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string PreviousEmployerName { get; set; }
        public string PreviousEmployerStreetAddress { get; set; }
        public string PreviousEmployerCity { get; set; }
        public string PreviousEmployerState { get; set; }
        public string PreviousEmployerZipcode { get; set; }
        public string EmploymentStartDate { get; set; }
        public string EmploymentEndDate { get; set; }
        public string ReasonForLeavingPreviousEmployments { get; set; }
        public string EmploymentGapFrom { get; set; }
        public string EmploymentGapTo { get; set; }
        public string ReasonForEmploymentGap { get; set; }

    }

    public class CurrentEmployer
    {
        public int CurrentEmployerDetailId { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string CurrentEmployerName { get; set; }
        public string CurrentEmployerStreetAddress { get; set; }
        public string CurrentEmployerCity { get; set; }
        public string CurrentEmployerState { get; set; }
        public string CurrentEmployerZipcode { get; set; }
        public string EmploymentStartDate { get; set; }
    }

    public class CurrentResidence
    {
        public int CurrentResidenceDetailID { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string CurrentResidenceStreetAddress { get; set; }
        public string CurrentResidenceCity { get; set; }
        public string CurrentResidenceState { get; set; }
        public string CurrentResidenceZipcode { get; set; }
        public string CurrentResidenceDuration { get; set; }

    }
    public class PreviousResidence
    {
        public int PreviousResidenceDetailID { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string PreviousResidenceStreetAddress { get; set; }
        public string PreviousResidenceCity { get; set; }
        public string PreviousResidenceState { get; set; }
        public string PreviousResidenceZipcode { get; set; }
        public string PreviousResidenceDuration { get; set; }

    }

    public class TypeOfEquipmentClass
    {
        public int TypeOfEquipmentDetailId { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public string TypeOfEquipment { get; set; }
        public string Miles { get; set; }
        public string DrivingFrom { get; set; }
        public string DrivingTo { get; set; }


    }

    public class TrafficConviction
    {
        public int TrafficConvictionsDetailID { get; set; }
        public string Location { get; set; }
        public string VehicleType { get; set; }
        public string DateOfConviction { get; set; }
        public string Charge { get; set; }
        public string Penalty { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public int? CountForFaxId { get; set; }
    }

    public class AccidentRecord
    {
        public int AccidentRecordDetailId { get; set; }
        public string NatureOfAccident { get; set; }
        public string DateOfAccident { get; set; }
        public string Fatalities { get; set; }
        public string Injuries { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public int? CountForFaxId { get; set; }
    }
    public class DriverLicenseStatus
    {
        public int DriverLicenseStatusId { get; set; }
        public string DriverLicenseStatusStatement { get; set; }
        public int? UserId { get; set; }
        public int? TempCPScreenDataID { get; set; }
        public int? CountForFaxId { get; set; }
    }

}