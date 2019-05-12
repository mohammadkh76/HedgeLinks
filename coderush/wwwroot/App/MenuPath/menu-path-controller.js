adminModule.controller("menuPathController", ["$scope", "dataService", "$window", "$uibModal", function ($scope, dataService, $window, $uibModal) {
    $scope.currentPage = 1;
    $scope.tableLoading = true;
    $scope.page={
        Current: $scope.currentPage,
        ItemInPage: 10
    }

    dataService.post('/api/Menupath/GetAll/',$scope.page).then(function (res) {
        $scope.tableLoading = false;
        console.log(res.data);
        if (res.data.Data.length>0) {
            $scope.data = res.data.Data;
            $scope.totalItems = res.data.Count;
        }
    })

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        dataService.post('/api/Menupath/GetAll/',$scope.page).then(function (res) {
            $scope.tableLoading = false;
            console.log(res.data);
            if (res.data.Data.length > 0) {
                $scope.data = res.data.Data;
                $scope.totalItems = res.data.Count;
            }
        })
    };
    $scope.maxSize = 5;
    $scope.openDescriptionModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/Menupath/DescriptionDetail/' + id;
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/MenuPath/description-menupath.html',
            controller: 'descriptionMenupathModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
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