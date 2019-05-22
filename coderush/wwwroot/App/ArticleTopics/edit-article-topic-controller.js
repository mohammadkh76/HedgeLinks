adminModule.controller('EditMenupathModalController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }


    $scope.tinymceOptions = {
        plugins: 'fullpage powerpaste searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount imagetools media link contextmenu colorpicker textpattern',
        toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat imageupload',
        setup: function (editor) {
            var inp = $('<input id="tinymce-uploader" type="file" name="pic" accept="image/*" style="display:none">');
            $(editor.getElement()).parent().append(inp);

            inp.on("change", function () {
                var input = inp.get(0);
                var file = input.files[0];
                var fr = new FileReader();
                fr.onload = function () {
                    var img = new Image();
                    img.src = fr.result;
                    editor.insertContent('<img src="' + img.src + '"/>');
                    inp.val('');
                }
                fr.readAsDataURL(file);
            });

            editor.addButton('imageupload', {
                text: "IMAGE",
                icon: false,
                onclick: function (e) {
                    inp.trigger('click');
                }
            });
        },
    };
    dataService.get($scope.url).then(function (res) {
        if (res.data.Status == "success") {
            $scope.edit = res.data.Data;
            $scope.Description = res.data.Data.Description;
            $scope.selectedId = res.data.Data.Id;
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        $scope.editData = {
            Id:$scope.selectedId,
            Name: $scope.edit.Name,
            Description: $scope.Description,
            PageName:$scope.edit.PageName,
        }
        dataService.post('/api/MenuPath/Edit/', $scope.editData).then(function (res) {
            if (res.data.Status == 'success') {
                $uibModalInstance.close();
            }

        })
        $uibModalInstance.result.then(function () {
            if (!$scope.isCanceled) {
                $scope.tableLoading = true;
                $scope.getAll({
                    successFunc() {
                        $scope.tableLoading = false;
                    },
                    messages() {
                        toaster.pop('success', 'Success', 'Your Record edited successfully');

                    }
                });

            }
           
        });
        $scope.cancel = function () {
            $scope.isCanceled = true;
            $uibModalInstance.close();

        }

    }
}])