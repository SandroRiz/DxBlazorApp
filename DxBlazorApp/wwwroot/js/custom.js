

var filemanager;
var baseUrl = "";



function renderFileManager(container) {

  
   
     var   arrayPermission = {
            "copy": true,
            "create": true,
            "download": true,
            "move": true,
            "delete": true,
            "upload": true,
            "rename": true
        };
    
   

    var provider = new DevExpress.fileManagement.RemoteFileSystemProvider({
        endpointUrl: baseUrl + "/api/file-manager-file-system/" 
    })

    filemanager = $(container).dxFileManager(
        {
            fileSystemProvider: provider,
            currentPath: "Widescreen",
            "permissions": arrayPermission,
            onSelectedFileOpened: function (e) {
                if (e.file.name.toLowerCase().endsWith("jpg") || e.file.name.toLowerCase().endsWith("png")) {
                    var popup = $("#photo-popup").dxPopup("instance");

                    popup.option({
                        "title": e.file.name,
                        "contentTemplate": "<img src=\"" + e.file.dataItem.url + "\" class=\"photo-popup-image\" />"
                    });
                    popup.show();
                }
            }

        });

    $("#photo-popup").dxPopup({
        maxHeight: 600,
        closeOnOutsideClick: true,
        onContentReady: function (e) {
            var $contentElement = e.component.content();
            $contentElement.addClass("photo-popup-content");
        }
    });
}

