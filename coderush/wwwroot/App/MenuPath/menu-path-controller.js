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
    $scope.data = [{
        Id: "1",
        Name: "About",
        Description: "This is the page",
        PageName: "About",
        Createdby: "Super@hedge.com",
        EditedBy: "",
        CreatedDate:"12/02/2019",
        EditedDate:"12/02/2019"
    }, {
            Name: "Services",
            Description: "This is the page",
            PageName: "About",
            Createdby: "Super@hedge.com",
            EditedBy: "",
            CreatedDate: "12/02/2019",
            EditedDate: "12/02/2019"
        }, {
            Name: "Contact",
            Description: "This is the page",
            PageName: "About",
            Createdby: "Super@hedge.com",
            EditedBy: "",
            CreatedDate: "12/02/2019",
            EditedDate: "12/02/2019"
        }, {
            Name: "Home",
            Description: "This is the page",
            PageName: "About",
            Createdby: "Super@hedge.com",
            EditedBy: "",
            CreatedDate: "12/02/2019",
            EditedDate: "12/02/2019"
        }
    ];

    dataService.get('/api/Menupath').then(function (res) {
        console.log(res.data);
        if (res.data.items.length>0) {
            $scope.data = res.data;
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
            templateUrl: '/App/DeleteModal/deleteModal.html',
            controller: 'deleteModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }

}]);