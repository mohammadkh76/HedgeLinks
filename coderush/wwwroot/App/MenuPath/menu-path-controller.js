adminModule.controller("menuPathController", ["$scope", "dataService", "$window", function ($scope, dataService, $window) {

            $scope.data = ej.DataManager({
                url: "/api/MenuPath/",
                adaptor: new ej.WebApiAdaptor(),
                offline: true,
               

            });  


    $scope.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, editMode: "dialogtemplate", dialogEditorTemplateID: "#template"};
        $scope.toolbarItems = {
            showToolbar: true, toolbarItems: ["add", "edit", "delete", "update", "cancel"]
        }
        debugger;

    


}]);