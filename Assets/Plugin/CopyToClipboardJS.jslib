mergeInto(LibraryManager.library, {
    CopyToClipboardJS: function(levelCode){
       levelCode = UTF8ToString(levelCode);
       var strToCopy = levelCode.replace(/[\u200B-\u200D\uFEFF]/g,'');
    
       navigator.clipboard.writeText(strToCopy).then(function() {
          console.log(strToCopy);
       }).catch(function(error) {
          console.error('Error copying level code into clipboard: ', error);
       });
    }
});