using MongoDB.Bson;
namespace CMSAPI.Models;

public class Policy
{
     public ObjectId _id { get; set; }
    public int pid { get; set; }
    public string pname { get; set; } = "";
    public string pdesc_short { get; set; } = "";    
    public string pdesc { get; set; } = "";
    public string ptype { get; set; } = "";
    public double pCoverage { get; set; }
    public double pPremium { get; set; }
    public string Gender { get; set; } = "";
    public string AgeGroup { get; set; } = "";
    public string Members { get; set; } = "";
    public string Insurer {get;set;}="";
    public int pgrade { get; set; }
    public string pstatus { get; set; } = "";

}
