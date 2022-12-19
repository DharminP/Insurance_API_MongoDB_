using CMSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using MongoDB.Driver;

namespace CMSAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class PolicyController : ControllerBase
{
    private IMongoRepository _iMongoRepository;

    public PolicyController(IMongoRepository iMongoRepository)
    {
        _iMongoRepository = iMongoRepository;
    }
    [HttpGet]
    public string GetAllPolicies()
    {
        return JsonSerializer.Serialize(_iMongoRepository.GetAllPolicy_M<Policy>("DB_INSURANCE", "col_policies"));
    }
    [HttpPost]
    public async Task CreateNewPolicy(Policy _policy)
    {
        //await InfluxDb();
        await _iMongoRepository.AddNewPolicy<Policy>("DB_INSURANCE", "col_policies", _policy);
    }

    [HttpGet("{pid:int}")]
    public string GetPolicyById(int pid)
    {
        var filter = Builders<Policy>.Filter.Eq("pid", pid);
        return JsonSerializer.Serialize(_iMongoRepository.GetFilteredPolicy<Policy>("DB_INSURANCE", "col_policies", filter));
    }

    [HttpGet("GetRelevantPolicyJson/{ptype}/{Gender}/{AgeGroup}/{Members}/{pgrade}")]
    public string GetRelevantPolicyJson(string ptype, string Gender, string AgeGroup, string Members, int pgrade)
    {
        var builder = Builders<Policy>.Filter;
        var filterPtype = Builders<Policy>.Filter.Eq(x => x.ptype, ptype);
        var filterGender = Builders<Policy>.Filter.Eq(x => x.Gender, Gender);
        var filterAgeGroup = Builders<Policy>.Filter.Eq(x => x.AgeGroup, AgeGroup);
        var filterMembers = Builders<Policy>.Filter.Eq(x => x.Members, Members);
        var filterpgrade = Builders<Policy>.Filter.Eq(x => x.pgrade, pgrade);

        var filter = builder.And(new[] { filterPtype, filterGender, filterAgeGroup, filterMembers, filterpgrade });//, filterAgeGroup, filterMembers, filterpgrade });

        // (Builders<Policy>.Filter.Eq(x => x.ptype, ptype) & Builders<Policy>.Filter.Eq(x => x.Gender, Gender) & Builders<Policy>.Filter.Eq(x => x.AgeGroup, AgeGroup) & Builders<Policy>.Filter.Eq(x => x.Members, Members) & Builders<Policy>.Filter.Eq(x => x.pgrade, pgrade));
        return JsonSerializer.Serialize(_iMongoRepository.GetFilteredPolicy<Policy>("DB_INSURANCE", "col_policies", filter));
    }

    [HttpGet("GetAllPoliciesJson")]
    public string GetAllPoliciesJson()
    {
        return JsonSerializer.Serialize(_iMongoRepository.GetAllPolicy_M<Policy>("DB_INSURANCE", "col_policies"));
    }

    [HttpDelete("{pid:int}")]
    public string DeletePolicy(int pid)
    {
        var filter = Builders<Policy>.Filter.Eq("pid", pid);
        var result = _iMongoRepository.DeletePolicy<Policy>("DB_INSURANCE", "col_policies", filter);
        if (result == true)
        {
            return JsonSerializer.Serialize(_iMongoRepository.GetAllPolicy_M<Policy>("DB_INSURANCE", "col_policies"));
        }
        return string.Empty;
    }
    [HttpPut]
    public async Task UpdatePolicy(Policy policy)
    {
        var filter = Builders<Policy>.Filter.Eq("pid", policy.pid);
        var _ToUpdate = Builders<Policy>.Update
            .Set(p => p.pname, policy.pname)
            .Set(p => p.ptype, policy.ptype)
            .Set(p => p.pgrade, policy.pgrade)
            .Set(p => p.pdesc_short, policy.pdesc_short)
            .Set(p => p.pgrade, policy.pgrade)
            .Set(p => p.pCoverage, policy.pCoverage)
            .Set(p => p.Insurer, policy.Insurer)
            .Set(p => p.pPremium, policy.pPremium)
            .Set(p => p.Gender, policy.Gender)
            .Set(p => p.AgeGroup, policy.AgeGroup)
            .Set(p => p.Members, policy.Members)
            .Set(p => p.pdesc, policy.pdesc)
            .Set(p => p.pstatus, policy.pstatus);

        await _iMongoRepository.UpdatePolicy<Policy>("DB_INSURANCE", "col_policies", filter, _ToUpdate);
    }
}