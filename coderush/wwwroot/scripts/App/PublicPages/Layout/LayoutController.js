angModule.controller('layoutController',['$scope','$window','dataService','encDec',function ($scope,$window,dataService,encDec) {
    dataService.get('/api/HomeApi/GetContact/').then(function (res) {
        $scope.mapPath='https://www.google.com/maps/embed/v1/place?q='+res.data.contactus.Latitude+','+res.data.contactus.Longitude+'&amp;key=AIzaSyAcTY0FFvVG3pEUt4BGStu6pVc72ZsyQP0&zoom=9';
        $scope.iframe=`<iframe src="${$scope.mapPath}" width="100%" height="250px" frameborder="0" style="border:0"></iframe>`;
        $(".map-section2").append($scope.iframe);
        $scope.contactus=res.data.contactus;
    });
    dataService.get('/api/LogoApi/GetLogo/').then(function (res) {
        $scope.logo=res.data.logo[0];
    });

}]);