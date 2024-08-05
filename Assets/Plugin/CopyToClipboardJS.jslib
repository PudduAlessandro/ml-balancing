mergeInto(LibraryManager.library, {
    CopyToClipboardJS: function(levelCode){
       var strToCopy = UTF8ToString(levelCode);
       strToCopy = strToCopy.replace(/\u200B/g,'');
    
       navigator.clipboard.writeText(strToCopy).then(function() {
          console.log(strToCopy);
       }).catch(function(error) {
          console.error('Error copying level code into clipboard: ', error);
       });
    }
});