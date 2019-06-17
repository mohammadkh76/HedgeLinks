layoutModule.config(function($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl : "/Views/Index.html",
            controller:"indexController"
            
        })
        .when("/red", {
            templateUrl : "red.htm"
        })
        .when("/green", {
            templateUrl : "green.htm"
        })
        .when("/blue", {
            templateUrl : "blue.htm"
        });
});