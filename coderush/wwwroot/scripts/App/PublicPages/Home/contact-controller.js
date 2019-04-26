angModule.controller('contactPageController',['$scope','dataService',function ($scope,dataService) {
    dataService.get('/api/HomeApi/GetContact/').then(function (res) {
        $scope.mapPath='https://www.google.com/maps/embed/v1/place?q='+res.data.contactus.Latitude+','+res.data.contactus.Longitude+'&amp;key=AIzaSyAcTY0FFvVG3pEUt4BGStu6pVc72ZsyQP0&zoom=9';
        $scope.iframe=`<iframe src="${$scope.mapPath}" width="500" height="400" frameborder="0" style="border:0"></iframe>`;
        $(".map-section").append($scope.iframe);
      $scope.data=res.data.contactus;
    });
}]);