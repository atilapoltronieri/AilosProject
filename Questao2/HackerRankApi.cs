using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Questao2
{
    public static class HackerRankApi
    {
        private const string ApiValue = "https://jsonmock.hackerrank.com/";
        private const string ApiResource = "api/football_matches";
        private const string ApiYearParameter = "year";
        private const string ApiTeam1Parameter = "team1";
        private const string ApiTeam2Parameter = "team2";
        private const string ApiPageParameter = "page";

        private const string JsonTotalPages = "total_pages";
        private const string JsonData = "data";
        private const string JsonTeam1Goals = "team1goals";
        private const string JsonTeam2Goals = "team2goals";


        public static int GetScore(string team, int year)
        {
            var goalsHome = GetGols(team, year, JsonTeam1Goals);

            var goalsAway = GetGols(team, year, JsonTeam2Goals, false);

            return goalsHome + goalsAway;
        }

        private static int GetGols(string team, int year, string jsonTeamGoals, bool isHome = true)
        {
            var dataHome = GetApiContentAsJson(year, team1: isHome ? team: string.Empty, team2 : !isHome ? team : string.Empty);
            var _ = int.TryParse(dataHome[JsonTotalPages].ToString(), out int totalPagesHome);

            // Get goals from page 1;
            var totalGoals = CalculateGoals(dataHome[JsonData], jsonTeamGoals);

            // Get goals from page 2 forward;
            for (var i = 2; i <= totalPagesHome; i++)
            {
                var getNewPage = GetApiContentAsJson(year, team1: isHome ? team : string.Empty, team2: !isHome ? team : string.Empty, page: i);
                totalGoals += CalculateGoals(getNewPage[JsonData], jsonTeamGoals);
            }

            return totalGoals;
        }

        private static int CalculateGoals(JsonNode data, string team1Or2)
        {
            var goals = 0;

            if (data == null || !data.AsArray().Any()) return goals;

            for (int i = 0; i < data.AsArray().Count; i++)
            {
                _ = int.TryParse(data[i][team1Or2].ToString(), out int goalsHome);
                goals+= goalsHome;
            }

            return goals;
        }

        //private static int G

        private static JsonNode GetApiContentAsJson(int year, string team1 = "", string team2 = "", int page = 0)
        {
            var client = new RestClient(ApiValue);
            
            var request = new RestRequest(ApiResource, Method.Get);
            request.AddParameters(team1, year, team2, page);
            
            var response = client.Execute(request);
            
            return JsonNode.Parse(response.Content);
        }

        private static void AddParameters(this RestRequest request, string team1, int year, string team2, int page)
        {
            request.AddParameter(ApiYearParameter, year);

            if (!string.IsNullOrWhiteSpace(team1))
                request.AddParameter(ApiTeam1Parameter, team1);

            if (!string.IsNullOrWhiteSpace(team2)) 
                request.AddParameter(ApiTeam2Parameter, team2);

            if (page > 0)
                request.AddParameter(ApiPageParameter, page);
        }
    }
}
