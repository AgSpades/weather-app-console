using dotenv.net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

var API = DotEnv.Read();
var API_KEY = API["API_KEY"];

