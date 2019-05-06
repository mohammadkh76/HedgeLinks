adminModule.controller("menuPathController", ["$scope", "dataService", "$window", "$uibModal", function ($scope, dataService, $window, $uibModal) {


//    var dataManager = ej.DataManager({
//        url: "/api/MenuPath",
//        adaptor: new ej.WebApiAdaptor(),
//        offline: true,
     

//    });

//    dataManager.ready.done(function (e) {
//        $scope.items = e.result;
//        $scope.$apply();
//})    //        dataSource: ej.DataManager({
    //            json: e.result,
    //            adaptor: new ej.remoteSaveAdaptor(),
    //            insertUrl: "/api/User/Insert",
    //            removeUrl: "/api/User/Remove",
    //            updateUrl: "/api/User/Update"

    //});
    //})

    dataService.get('/api/Menupath/GetAll').then(function (res) {
        console.log(res.data);
        if (res.data.Data.length>0) {
            $scope.data = res.data.Data;
        }
    })
    $scope.totalItems = 64;
    $scope.currentPage = 4;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        $log.log('Page changed to: ' + $scope.currentPage);
    };
    $scope.maxSize = 5;
    $scope.bigTotalItems = 175;
    $scope.bigCurrentPage = 1;

    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/Menupath/Delete/' + id;
        $scope.href = '/Menupaths/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/DeleteModal/delete-modal.html',
            controller: 'deleteModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }

    $scope.openInsertModal = function () {
        $scope.url = '/api/Menupath/Insert/';
        $scope.href = '/Menupaths/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/Menupath/insert-menupath.html',
            controller: 'InsertMenupathController',
            scope: $scope,
            size: 'lg',
            windowClass: 'insert-modal',
            appendTo: $('body')
        })
    }
    $scope.openEditModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/Menupath/Edit/' + id;
        $scope.href = '/Menupaths/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/Menupath/EditModal.html',
            controller: 'EditMenupathModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
    

}]);