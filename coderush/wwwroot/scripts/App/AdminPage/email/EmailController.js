angModule.controller('emailController',['$scope','dataService','$uibModal',function ($scope,dataService,$uibModal) {
    $scope.getEmailList=function(page){
        dataService.post('/api/EmailApi/GetEmail/',page).then(function (res) {
            $scope.totalItems=res.data.emailCount;
            $scope.email=res.data.email;
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
        $scope.url='/api/EmailApi/DelEmail/'+id;
        $scope.href='/Emails/index';
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