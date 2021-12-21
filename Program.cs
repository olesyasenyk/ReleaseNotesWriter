using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace ReleaseNotesWriter
{
    static class ExtensionMethods
    {
        public static void Print<T>(this IEnumerable<T> sequence, Action<T> printAction)
        {
            foreach (T item in sequence)
            {
                printAction(item);
            }
        }
    }

    public class RootobjectGet
    {
        public string expand { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public Issue[] issues { get; set; }
    }

    public class Issue
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
        public override string ToString() => $"{self}\n{key}\n{fields}";
    }

    public class Fields
    {
        public Customfield_13901 customfield_13901 { get; set; }//team
        public string summary { get; set; }
        public string description { get; set; }
        public Customfield_11900 customfield_11900 { get; set; }//developer
        public Customfield_11902 customfield_11902 { get; set; }//tester
        public Issuetype issuetype { get; set; }
        public Status status { get; set; }
        public string[] labels { get; set; }
        public override string ToString()
        {
            StringBuilder builder = new();

            foreach (string label in labels)
            {
                builder.Append($"{label}\n"); ;
            }
            return $"{customfield_13901}\n{description}\n{customfield_11900}\n{customfield_11902}\n{issuetype}\n{status}\n{builder}";
        }
    }

    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public override string ToString() => $"{name}";
    }

    public class Customfield_13901
    {
        public string value { get; set; }
        public override string ToString() => $"{value}";
    }

    public class Customfield_11900
    {
        public string displayName { get; set; }
        public override string ToString() => $"{displayName}";
    }

    public class Customfield_11902
    {
        public string displayName { get; set; }
        public override string ToString() => $"{displayName}";
    }

    public class Issuetype
    {
        public string name { get; set; }
        public override string ToString() => $"{name}";
    }

    class Ticket
    {
        public string Team { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ResponsiblePerson { get; set; }

        public string Type { get; set; }

        public string Access { get; set; }

        public int No { get; set; }

        public string Demo { get; set; }

        public string BusTecOps { get; set; }

        public override string ToString() => $"{Team}\n{Number}\n{Title}\n{Description}\n{ResponsiblePerson}\n{Type}\n{Access}\n";
    }
    class DeployFocusTasks
    {
        public string jql { get; set; }
        public int maxResults { get; set; }
    }

    public class Rootobject
    {
        public Result[] results { get; set; }
    }

    public class Result
    { 
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public Space space { get; set; }
        public History history { get; set; }
        public Version version { get; set; }
        public Ancestor[] ancestors { get; set; }
        public Container container { get; set; }
        public Macrorenderedoutput macroRenderedOutput { get; set; }
        public Body body { get; set; }
        public Extensions extensions { get; set; }
        public _Expandable9 _expandable { get; set; }
        public _Links7 _links { get; set; }
    }

    public class Rootobject1
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public Space space { get; set; }
        public History history { get; set; }
        public Version version { get; set; }
        public Ancestor[] ancestors { get; set; }
        public Container container { get; set; }
        public Macrorenderedoutput macroRenderedOutput { get; set; }
        public Body1 body { get; set; }
        public Extensions extensions { get; set; }
        public _Expandable9 _expandable { get; set; }
        public _Links7 _links { get; set; }
    }
    public class Space
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public _Expandable _expandable { get; set; }
        public _Links _links { get; set; }
    }

    public class _Expandable
    {
        public string settings { get; set; }
        public string metadata { get; set; }
        public string operations { get; set; }
        public string lookAndFeel { get; set; }
        public string identifiers { get; set; }
        public string permissions { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public string theme { get; set; }
        public string history { get; set; }
        public string homepage { get; set; }
    }

    public class _Links
    {
        public string webui { get; set; }
        public string self { get; set; }
    }

    public class History
    {
        public bool latest { get; set; }
        public Createdby createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public _Expandable2 _expandable { get; set; }
        public _Links2 _links { get; set; }
    }

    public class Createdby
    {
        public string type { get; set; }
        public string accountId { get; set; }
        public string accountType { get; set; }
        public string email { get; set; }
        public string publicName { get; set; }
        public Profilepicture profilePicture { get; set; }
        public string displayName { get; set; }
        public bool isExternalCollaborator { get; set; }
        public _Expandable1 _expandable { get; set; }
        public _Links1 _links { get; set; }
    }

    public class Profilepicture
    {
        public string path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool isDefault { get; set; }
    }

    public class _Expandable1
    {
        public string operations { get; set; }
        public string personalSpace { get; set; }
    }

    public class _Links1
    {
        public string self { get; set; }
    }

    public class _Expandable2
    {
        public string lastUpdated { get; set; }
        public string previousVersion { get; set; }
        public string contributors { get; set; }
        public string nextVersion { get; set; }
    }

    public class _Links2
    {
        public string self { get; set; }
    }

    public class Version
    {
        public By by { get; set; }
        public DateTime when { get; set; }
        public string friendlyWhen { get; set; }
        public string message { get; set; }
        public int number { get; set; }
        public bool minorEdit { get; set; }
        public string syncRev { get; set; }
        public string syncRevSource { get; set; }
        public string confRev { get; set; }
        public bool contentTypeModified { get; set; }
        public _Expandable4 _expandable { get; set; }
        public _Links4 _links { get; set; }
    }

    public class By
    {
        public string type { get; set; }
        public string accountId { get; set; }
        public string accountType { get; set; }
        public string email { get; set; }
        public string publicName { get; set; }
        public Profilepicture1 profilePicture { get; set; }
        public string displayName { get; set; }
        public bool isExternalCollaborator { get; set; }
        public _Expandable3 _expandable { get; set; }
        public _Links3 _links { get; set; }
    }

    public class Profilepicture1
    {
        public string path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool isDefault { get; set; }
    }

    public class _Expandable3
    {
        public string operations { get; set; }
        public string personalSpace { get; set; }
    }

    public class _Links3
    {
        public string self { get; set; }
    }

    public class _Expandable4
    {
        public string collaborators { get; set; }
        public string content { get; set; }
    }

    public class _Links4
    {
        public string self { get; set; }
    }

    public class Container
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public History1 history { get; set; }
        public _Expandable6 _expandable { get; set; }
        public _Links6 _links { get; set; }
    }

    public class History1
    {
        public Createdby1 createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class Createdby1
    {
        public string type { get; set; }
        public string accountId { get; set; }
        public string accountType { get; set; }
        public string email { get; set; }
        public string publicName { get; set; }
        public Profilepicture2 profilePicture { get; set; }
        public string displayName { get; set; }
        public bool isExternalCollaborator { get; set; }
        public _Expandable5 _expandable { get; set; }
        public _Links5 _links { get; set; }
    }

    public class Profilepicture2
    {
        public string path { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool isDefault { get; set; }
    }

    public class _Expandable5
    {
        public string operations { get; set; }
        public string personalSpace { get; set; }
    }

    public class _Links5
    {
        public string self { get; set; }
    }

    public class _Expandable6
    {
        public string settings { get; set; }
        public string metadata { get; set; }
        public string operations { get; set; }
        public string lookAndFeel { get; set; }
        public string identifiers { get; set; }
        public string permissions { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public string theme { get; set; }
        public string homepage { get; set; }
    }

    public class _Links6
    {
        public string webui { get; set; }
        public string self { get; set; }
    }

    public class Macrorenderedoutput
    {
    }

    public class Body
    {
        public View view { get; set; }
        public _Expandable8 _expandable { get; set; }
    }

    public class View
    {
        public string value { get; set; }
        public string representation { get; set; }
        public object[] embeddedContent { get; set; }
        public _Expandable7 _expandable { get; set; }
    }

    public class Body1
    {
        public Storage storage { get; set; }
        public _Expandable8 _expandable { get; set; }
    }

    public class Storage
    {
        public string value { get; set; }
        public string representation { get; set; }
        public object[] embeddedContent { get; set; }
        public _Expandable7 _expandable { get; set; }
    }

    public class _Expandable7
    {
        public string content { get; set; }
    }

    public class _Expandable8
    {
        public string editor { get; set; }
        public string atlas_doc_format { get; set; }
        public string view { get; set; }
        public string export_view { get; set; }
        public string styled_view { get; set; }
        public string dynamic { get; set; }
        public string editor2 { get; set; }
        public string anonymous_export_view { get; set; }
    }

    public class Extensions
    {
        public int position { get; set; }
    }

    public class _Expandable9
    {
        public string childTypes { get; set; }
        public string metadata { get; set; }
        public string operations { get; set; }
        public string schedulePublishDate { get; set; }
        public string children { get; set; }
        public string restrictions { get; set; }
        public string descendants { get; set; }
    }

    public class _Links7
    {
        public string editui { get; set; }
        public string webui { get; set; }
        public string context { get; set; }
        public string self { get; set; }
        public string tinyui { get; set; }
        public string collection { get; set; }
        public string _base { get; set; }
    }

    public class Ancestor
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public Macrorenderedoutput1 macroRenderedOutput { get; set; }
        public Extensions1 extensions { get; set; }
        public _Expandable10 _expandable { get; set; }
        public _Links8 _links { get; set; }
    }

    public class Macrorenderedoutput1
    {
    }

    public class Extensions1
    {
        public int position { get; set; }
    }

    public class _Expandable10
    {
        public string container { get; set; }
        public string metadata { get; set; }
        public string restrictions { get; set; }
        public string history { get; set; }
        public string body { get; set; }
        public string version { get; set; }
        public string descendants { get; set; }
        public string space { get; set; }
        public string childTypes { get; set; }
        public string operations { get; set; }
        public string schedulePublishDate { get; set; }
        public string children { get; set; }
        public string ancestors { get; set; }
    }

    public class _Links8
    {
        public string self { get; set; }
        public string tinyui { get; set; }
        public string editui { get; set; }
        public string webui { get; set; }
    }
    class Program
    {
        private static byte[] AuthenticationBytes => Encoding.ASCII.GetBytes("olesia.falafivka@everymatrix.com:xGXgyuJchkFWnzHfoPzf4D97");

        private static async Task<HttpResponseMessage> GetContentFromJira(string link, HttpContent content)
        {
            using HttpClient confClient = new();
            confClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(AuthenticationBytes));
            return await confClient.PostAsync(link, content);
        }

        private static async Task<HttpResponseMessage> PutContent(string link, HttpContent content)
        {
            using HttpClient confClient = new();
            confClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(AuthenticationBytes));
            return await confClient.PutAsync(link, content);
        }

        private static async Task<HttpResponseMessage> GetContentFromConfluence(string link)
        {
            using HttpClient confClient = new();
            confClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(AuthenticationBytes));
            return await confClient.GetAsync(link);
        }

        private static async Task Main()
        {//STEP 1: GET TICKETS FROM JIRA
            string linkToTasks = "https://everymatrix.atlassian.net/rest/api/2/search";

            var deployFocusTasks = new DeployFocusTasks
            {
                jql = "project = MON AND (\"Team MON\" in (\"Back Office\", UCS_Rep, \"Vendor Integration\", MMFE, Recon, Tech) OR \"Team MON\" is EMPTY) AND issuetype in (subTaskIssueTypes(), Bug, Epic, \"New Feature\", \"Roadmap Feature\", Story, \"Tech Task\", Design) AND status in (\"Ready for Prod\", \"Testing (Stage)\", \"Ready for Test (STAGE)\", \"Testing (DEV)\", \"Ready for Test (DEV)\", Review, \"In Progress\") AND labels = NextDeployFocus",
                maxResults = 100
            };

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(deployFocusTasks, serializeOptions);

            HttpContent requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage message = await GetContentFromJira(linkToTasks, requestBody);

            if (!message.IsSuccessStatusCode)
            {
                Console.WriteLine("Unable to reach JIRA");
                return;
            }

            //string response = await message.Content.ReadAsStringAsync();
            //await File.WriteAllTextAsync("board.json", response); // write response to file

            //string fileName = "tasks.json";
            //string jsonString = File.ReadAllText(fileName); // read response from file

            string jsonString = await message.Content.ReadAsStringAsync();

            Console.OutputEncoding = Encoding.UTF8;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<RootobjectGet>(jsonString, options);

            List<Ticket> backEnd = new();
            List<Ticket> frontEnd = new();
            List<Ticket> vendorIntegration = new();

            foreach (Issue issue in response.issues)
            {
                Ticket ticket = new() 
                {
                    Team = $"{issue.fields.customfield_13901}",
                    Number = $"https://everymatrix.atlassian.net/browse/{issue.key}",
                    Title = $"{issue.fields.summary}",
                    Description = $"{issue.fields.description}",
                    ResponsiblePerson = $"{issue.fields.customfield_11900}\n{issue.fields.customfield_11902}",
                    Type = $"{issue.fields.issuetype}" == "Bug" ? "Fix" : "Addition",
                    Demo = String.Empty,
                    Access = "EXT",
                    BusTecOps = String.Empty
                };

                if (ticket.Team == "Back Office")
                {
                    backEnd.Add(ticket);
                }

                if (ticket.Team == "MMFE")
                {
                    frontEnd.Add(ticket);
                }

                if (ticket.Team == "Vendor Integration")
                {
                    vendorIntegration.Add(ticket);
                }
            }

            IOrderedEnumerable<Ticket> sortedBackEnd = backEnd
                   .OrderBy(o => o.Type)
                   .ThenBy(t => t.Number);

            IOrderedEnumerable<Ticket> sortedFrontEnd = frontEnd
                    .OrderBy(o => o.Type)
                    .ThenBy(t => t.Number);

            IOrderedEnumerable<Ticket> sortedVendorIntegration = vendorIntegration
                    .OrderBy(o => o.Type)
                    .ThenBy(t => t.Number);

            //StringBuilder builder = new("BACK END\n");

            //foreach (Ticket ticket in sortedBackEnd)
            //{
            //    builder.Append($"{ticket.No}\n{ticket}\n");
            //}

            //builder.Append($"FRONT END\n");

            //foreach (Ticket ticket in sortedFrontEnd)
            //{
            //    builder.Append($"{ticket}\n");
            //}

            //builder.Append($"VENDOR INTEGRATION\n");

            //foreach (Ticket ticket in sortedVendorIntegration)
            //{
            //    builder.Append($"{ticket}\n");
            //}

            //Console.WriteLine(builder);


            //STEP 2: GET DETAILS OF THE PAGE TO WHICH TO POST //
            Console.WriteLine("Enter the link where to post:");
            string linkForPosting = Console.ReadLine();

            int pagesIndex = linkForPosting.IndexOf("pages/");
            int lastSlashIndex = linkForPosting.LastIndexOf("/");
            string pageID = linkForPosting[(pagesIndex + 6)..lastSlashIndex];

            string linkForGetRequest = $"https://everymatrix.atlassian.net/wiki/rest/api/content?spaceKey=MON&id={pageID}&expand=space,body.view,version,container";//the linkForPosting needs to be transformed for a GET request, using which we know the version 

            HttpResponseMessage message1 = await GetContentFromConfluence(linkForGetRequest);

            if (!message1.IsSuccessStatusCode)
            {
                Console.WriteLine("Unable to reach Confluence");
                return;
            }

            string confluenceString = await message1.Content.ReadAsStringAsync();

            var confluencePageData = JsonSerializer.Deserialize<Rootobject>(confluenceString, options);// this JSON has a "results" field - Class Rootobject

            int pageVersion = confluencePageData.results[0].version.number;

            //STEP 3: FORM CONTENT TO POST
            var dataToPost = JsonSerializer.Deserialize<Rootobject1>(File.ReadAllText("shortened.txt")); // JSON we need to post doesn't have "results" field - Class Rootobject1

            dataToPost.id = $"{pageID}";
            Console.WriteLine("Enter the title of the new page, for example Release notes 2021-10-25:");
            dataToPost.title = Console.ReadLine();
            dataToPost.version.number = pageVersion + 1;

            //Step 3.1. Form tables to post
            var htmlParser = new HtmlParser();
            var tablesToPost = await htmlParser.ParseDocumentAsync(File.ReadAllText("HTMLPage.html"));

            var backEndTable = tablesToPost.QuerySelectorAll("table[data-layout]")[0];

            int ticketNo = 1;

            foreach (Ticket ticket in sortedBackEnd)
            {
                var tr = tablesToPost.CreateElement("tr");
                var tbodyRef = backEndTable.GetElementsByTagName("tbody")[0];

                // Insert a row at the end of table
                var newRow = tbodyRef.AppendChild(tr);

                var numberingColumn = tablesToPost.CreateElement("td");
                numberingColumn.ClassName = "numberingColumn";
                numberingColumn.TextContent = $"{ticketNo}";
                tr.AppendChild(numberingColumn);
                ticketNo++;

                var jiraNumber = tablesToPost.CreateElement("td");
                jiraNumber.TextContent = $"{ticket.Number}";
                tr.AppendChild(jiraNumber);

                var description = tablesToPost.CreateElement("td");
                description.TextContent = $"{ticket.Description}";
                tr.AppendChild(description);

                var responsiblePerson = tablesToPost.CreateElement("td");
                responsiblePerson.TextContent = $"{ticket.ResponsiblePerson}";
                tr.AppendChild(responsiblePerson);

                var type = tablesToPost.CreateElement("td");
                type.TextContent = $"{ticket.Type}";
                tr.AppendChild(type);

                var summary = tablesToPost.CreateElement("td");
                summary.TextContent = $"{ticket.Description}";
                tr.AppendChild(summary);

                var demo = tablesToPost.CreateElement("td");
                demo.TextContent = $"{ticket.Demo}";
                tr.AppendChild(demo);

                var access = tablesToPost.CreateElement("td");
                access.TextContent = $"{ticket.Access}";
                tr.AppendChild(access);

                var busTecOps = tablesToPost.CreateElement("td");
                busTecOps.TextContent = $"{ticket.BusTecOps}";
                tr.AppendChild(busTecOps);

                //foreach (PropertyInfo prop in ticket.GetType().GetProperties()) //EXPLAIN + where mistake + maybe need to add p?
                //{
                //    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                //}
            }

            var frontEndTable = tablesToPost.QuerySelectorAll("table[data-layout]")[1];

            ticketNo = 1;

            foreach (Ticket ticket in sortedFrontEnd)
            {
                var tr = tablesToPost.CreateElement("tr");
                var tbodyRef = frontEndTable.GetElementsByTagName("tbody")[0];

                var newRow = tbodyRef.AppendChild(tr);

                var numberingColumn = tablesToPost.CreateElement("td");
                numberingColumn.ClassName = "numberingColumn";
                numberingColumn.TextContent = $"{ticketNo}";
                tr.AppendChild(numberingColumn);
                ticketNo++;

                var jiraNumber = tablesToPost.CreateElement("td");
                jiraNumber.TextContent = $"{ticket.Number}";
                tr.AppendChild(jiraNumber);

                var description = tablesToPost.CreateElement("td");
                description.TextContent = $"{ticket.Description}";
                tr.AppendChild(description);

                var responsiblePerson = tablesToPost.CreateElement("td");
                responsiblePerson.TextContent = $"{ticket.ResponsiblePerson}";
                tr.AppendChild(responsiblePerson);

                var type = tablesToPost.CreateElement("td");
                type.TextContent = $"{ticket.Type}";
                tr.AppendChild(type);

                var summary = tablesToPost.CreateElement("td");
                summary.TextContent = $"{ticket.Description}";
                tr.AppendChild(summary);

                var demo = tablesToPost.CreateElement("td");
                demo.TextContent = $"{ticket.Demo}";
                tr.AppendChild(demo);

                var access = tablesToPost.CreateElement("td");
                access.TextContent = $"{ticket.Access}";
                tr.AppendChild(access);

                var busTecOps = tablesToPost.CreateElement("td");
                busTecOps.TextContent = $"{ticket.BusTecOps}";
                tr.AppendChild(busTecOps);
            }

            var vendorIntegrationTable = tablesToPost.QuerySelectorAll("table[data-layout]")[2];

            ticketNo = 1;

            foreach (Ticket ticket in sortedVendorIntegration)
            {
                var tr = tablesToPost.CreateElement("tr");
                var tbodyRef = vendorIntegrationTable.GetElementsByTagName("tbody")[0];

                var newRow = tbodyRef.AppendChild(tr);

                var numberingColumn = tablesToPost.CreateElement("td");
                numberingColumn.ClassName = "numberingColumn";
                numberingColumn.TextContent = $"{ticketNo}";
                tr.AppendChild(numberingColumn);
                ticketNo++;

                var jiraNumber = tablesToPost.CreateElement("td");
                jiraNumber.TextContent = $"{ticket.Number}";
                tr.AppendChild(jiraNumber);

                var description = tablesToPost.CreateElement("td");
                description.TextContent = $"{ticket.Description}";
                tr.AppendChild(description);

                var responsiblePerson = tablesToPost.CreateElement("td");
                responsiblePerson.TextContent = $"{ticket.ResponsiblePerson}";
                tr.AppendChild(responsiblePerson);

                var type = tablesToPost.CreateElement("td");
                type.TextContent = $"{ticket.Type}";
                tr.AppendChild(type);

                var summary = tablesToPost.CreateElement("td");
                summary.TextContent = $"{ticket.Description}";
                tr.AppendChild(summary);

                var demo = tablesToPost.CreateElement("td");
                demo.TextContent = $"{ticket.Demo}";
                tr.AppendChild(demo);

                var access = tablesToPost.CreateElement("td");
                access.TextContent = $"{ticket.Access}";
                tr.AppendChild(access);

                var busTecOps = tablesToPost.CreateElement("td");
                busTecOps.TextContent = $"{ticket.BusTecOps}";
                tr.AppendChild(busTecOps);
            }

            string htmlToPost = @$"<h2>Back Office, Hosted Cashier</h2><h3>Back end</h3>{backEndTable.OuterHtml}<h3>Front end</h3>{frontEndTable.OuterHtml}<h3>Vendor integration</h3>{vendorIntegrationTable.OuterHtml}";

            await File.WriteAllTextAsync("htmlToPost.html", htmlToPost);

            dataToPost.body.storage.value = htmlToPost;

            //STEP 4: POST

            string linkToPost = $"https://everymatrix.atlassian.net/wiki/rest/api/content/{pageID}/?status=draft&action=publish";

            string jsonToPost = JsonSerializer.Serialize(dataToPost, serializeOptions);

            //Console.WriteLine(jsonToPost);

            HttpContent request = new StringContent(jsonToPost, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await PutContent(linkToPost, request);

            Console.WriteLine(responseMessage);

            var res = await responseMessage.Content.ReadAsStringAsync();//reads the reason of fail

            Console.WriteLine(res);
        }
    }
}

// We get MESSAGE, read it as STRING, deserialize it into JSON (var) to get necessary data easily
// We have a JSON (var), serialize it into STRING, send it in a request


