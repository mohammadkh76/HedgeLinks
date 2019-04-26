angModule.controller('sendEmailController',['$scope','dataService','$uibModal',function ($scope,dataService,$uibModal) {
    $scope.getEmailList=function(page){
        dataService.post('/api/SendEmailApi/GetSendEmail/',page).then(function (res) {
            $scope.totalItems=res.data.sendEmailCount;
            $scope.sendemail=res.data.sendEmail;
        })
    }
    $scope.page= {
        take: 10,
        page: 1,
        takeAll: false
    }
    $scope.getEmailList($scope.page);
    $scope.pageChanged = function() {
        $scope.page.Take=10;
        $scope.page.Page=$scope.currentPage;
        $scope.getEmailList($scope.page);
    };



    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url='/api/SendEmailApi/DelSendEmail/'+id;
        $scope.href='/SendEmails/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/Scripts/App/deleteModal/deleteModal.html',
            controller: 'deleteModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
}]);