angModule.controller('editMenuPathController', ['$scope', 'dataService', 'Upload', '$uibModal', '$window', function ($scope, dataService, Upload, $uibModal, $window) {
   
    $scope.pathDisabled = false;

    const urlPath = $window.location.pathname;
    var x = urlPath.split('/');
  
    dataService.get('/api/MenuPathApi/GetEditMenuPath/' + x[3]).then(function (res) {
        $scope.data = res.data.rec;
        $('#myTextArea').html(res.data.rec.Description);
    }).catch(function (err) {
        console.log(err.data)
    });
    $scope.submit = function (data) {
        $scope.Description=$("#myTextArea").tinymce().getContent();
        debugger;
        $scope.data.Description=$scope.Description;
        $scope.EMVM = {
            Id: x[3],
            Title: data.Title,
            Description: data.Description,
            PageName: data.PageName,
        };
        dataService.post('/api/MenuPathApi/EditMenuPath/', $scope.EMVM).then(function (res) {
            $window.location.href = '/MenuPaths/Index';

        })


    };
   
}]);