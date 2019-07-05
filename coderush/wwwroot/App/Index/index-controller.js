layoutModule.controller("indexController", ["$scope", "dataService", function ($scope, dataService) {

    dataService.get('api/TopImage/GetAllTopImage/').then(function (res) {
    $scope.topImage=res.data.Data
    console.log('GetAllTopImage',res.data.Data);

})
dataService.get('api/CommercialTips/GetAllCommercialTips/').then(function (res) {
    $scope.commercialTips = res.data.Data
    console.log('GetAllCommercialTips',res.data.Data);

})
dataService.get('api/JobIndustry/GetAllJobIndustry/').then(function (res) {
    $scope.jobIndustry = res.data.Data
    console.log('GetAllCommercialTips',res.data.Data);

})
dataService.get('api/JobIndustry/GetAllShowedJobIndustry/').then(function (res) {
    $scope.showedJobIndustry = res.data.Data
    console.log('GetAllShowedJobIndustry',res.data.Data);

})
dataService.get('api/Job/GetAllJobCompensation').then(function (res) {
    $scope.compensation = res.data.Data;
    console.log('GetAllJobCompensation',res.data.Data)

})
dataService.get('api/ArticleTopic/GetAllShowedArticleTopic/').then(function (res) {
    $scope.articlTopic = res.data.Data;
    console.log('GetAllShowedArticleTopic',res.data.Data)

})
dataService.get('api/ThirdSection/GetAllThirdSection/').then(function (res) {
    $scope.thirdSection = res.data.Data;
    console.log('GetAllThirdSection',res.data.Data)

})


}]);
