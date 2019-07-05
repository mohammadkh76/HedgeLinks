layoutModule.config(function($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl : "/Views/Index.html",
            controller:"indexController"
            
        })
        .when("/job-seeker-register", {
            templateUrl : "/Views/job-seeker-register.html",
            controller:"jobSeekerRegisterController"


        })
        .when("/green", {
            templateUrl : "green.htm"
        })
        .when("/blue", {
            templateUrl : "blue.htm"
        });
});