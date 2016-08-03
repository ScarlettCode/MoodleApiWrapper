# MoodleApiWrapper
Moodle Api Wrapper for C#

#Usage 

         
         async void DoSomething()
         {
            // Set your host 
            ApiWrapper.Host = new Uri("https://yourhost.example.com/");
         
            // request your token
            var token = await ApiWrapper.GetApiToken("username", "password", "services");
         
            //set your token
            ApiWrapper.ApiToken = token.Data.token;
         
            //make a call
            var result = await ApiWrapper.UpdateUser(1, username: "SuperAwesomeName");
         }

   
   
   
