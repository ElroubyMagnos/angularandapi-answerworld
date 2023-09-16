const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:45623';

const PROXY_CONFIG = [
  {
    context: [
      "/Home",
      "/Account",
      "/Check",
      "/SignUp",
      "/Cat",
      "/CatCheck",
      "/AddQues",
      "/GetQuestions",
      "/Bio",
      "/GetOneQues",
      "/AddComment",
      "/GetComments",
      "/QuesVote",
      "/VottingQues",
      "/RandomQues",
      "/TopAnswers",
      "/TopQuestions",
      "/AnsVote",
      "/VottingAns",
      "/AddFollow",
      "/CheckFollow",
      "/GetPMessages",
      "/ProfileQuestions",
      "/GetPComments",
      "/SignedIn",
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
