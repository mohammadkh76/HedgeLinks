adminModule.controller("menuPathController", ["$scope", "dataService", "$window", function ($scope, dataService, $window) {


    var dataManager = ej.DataManager({
        url: "/api/MenuPath",
        adaptor: new ej.WebApiAdaptor(),
        offline: true,
     

    });

    dataManager.ready.done(function (e) {
        $scope.items = e.result;
        $scope.$apply();
})    //        dataSource: ej.DataManager({
    //            json: e.result,
    //            adaptor: new ej.remoteSaveAdaptor(),
    //            insertUrl: "/api/User/Insert",
    //            removeUrl: "/api/User/Remove",
    //            updateUrl: "/api/User/Update"

    //});
    //})
    console.log(dataManager)
   
    $scope.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, editMode: "dialogtemplate", dialogEditorTemplateID: "#template" };
    $scope.toolbarItems = {
        showToolbar: true, toolbarItems: ["add", "edit", "delete", "update", "cancel"]
    }




}]);