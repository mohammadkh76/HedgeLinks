module.config(['$routeProvider','$locationProvider',function($routeProvider,$locationProvider){
    $routeProvider
       
        .when("/", {
            templateUrl : "/App/Visa/section-1.html",
          
        })
        .when("/section2", {
            templateUrl : "/App/Visa/section-2.html",
            resolve:{
                checkPageState:['RedirectToRoute',function (RedirectToRoute) {
                    if (!RedirectToRoute.pageState){
                        window.location.href='#/';
                    } 
                }] 
                   
            }
        })
        .when("/section3", {
            templateUrl : "/App/Visa/section-3.html",
            resolve:{
                checkPageState:['RedirectToRoute',function (RedirectToRoute) {
                    if (!RedirectToRoute.pageState){
                        window.location.href='#/';
                    }
                }]

            }
        })
        .when("/section4", {
            templateUrl : "/App/Visa/section-4.html",
            resolve:{
                checkPageState:['RedirectToRoute',function (RedirectToRoute) {
                    if (!RedirectToRoute.pageState){
                        window.location.href='#/';
                    }
                }]

            }
        })
        .when("/section5", {
            templateUrl : "/App/Visa/section-5.html",
            resolve:{
                checkPageState:['RedirectToRoute',function (RedirectToRoute) {
                    if (!RedirectToRoute.pageState){
                        window.location.href='#/';
                    }
                }]

            }
        });;
    $locationProvider.hashPrefix('');
} ]) 
    
module.controller("SectionOneController", function ($scope) {
    $scope.isRefresh= 1;
});
module.controller("SectionTwoController", function ($scope) {
    $scope.msg = 2;
});
module.controller("SectionThreeController", function ($scope) {
    $scope.msg = 3;
});
module.controller("SectionFourController", function ($scope) {
    $scope.msg = 4;
});

module.controller("SectionFiveController", function ($scope) {
    $scope.msg = 5;
});