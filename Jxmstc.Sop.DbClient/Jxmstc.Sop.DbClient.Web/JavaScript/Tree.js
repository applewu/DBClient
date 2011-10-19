var isArray = function(v) {
    return v && typeof v.length == 'number' && typeof v.splice == 'function';
}
var Node = function(attributes) {

    this.attributes = attributes || {};
    this.leaf = this.attributes.leaf;
    this.childNodes = [];
    if (!this.childNodes.indexOf) { // indexOf is a must
        this.childNodes.indexOf = function(o) {
            for (var i = 0, len = this.length; i < len; i++) {
                if (this[i] == o) {
                    return i;
                }
            }
            return -1;
        };
    }
    this.parentNode = null;
    this.firstChild = null;
    this.lastChild = null;
    this.previousSibling = null;
    this.nextSibling = null;
};

Node.prototype = {

    isLeaf: function() {
        return this.leaf === true;
    },
    setFirstChild: function(node) {
        this.firstChild = node;
    },
    setLastChild: function(node) {
        this.lastChild = node;
    },
    isLast: function() {
        return (!this.parentNode ? true : this.parentNode.lastChild == this);
    },

    isFirst: function() {
        return (!this.parentNode ? true : this.parentNode.firstChild == this);
    },
    hasChildNodes: function() {
        return !this.isLeaf() && this.childNodes.length > 0;
    },
    appendChild: function(node) {

        var index = this.childNodes.length;
        //  var oldParent = node.parentNode;
        //   oldParent.removeChild(node);

        index = this.childNodes.length;
        if (index === 0) {
            this.setFirstChild(node);
        }
        this.childNodes.push(node);
        node.parentNode = this;
        var ps = this.childNodes[index - 1];
        if (ps) {
            node.previousSibling = ps;
            ps.nextSibling = node;
        } else {
            node.previousSibling = null;
        }
        node.nextSibling = null;
        this.setLastChild(node);
        return node;
    },

    removeChild: function(node) {
        var index = this.childNodes.indexOf(node);
        if (index == -1) {
            return false;
        }

        // remove it from childNodes collection
        this.childNodes.splice(index, 1);

        // update siblings
        if (node.previousSibling) {
            node.previousSibling.nextSibling = node.nextSibling;
        }
        if (node.nextSibling) {
            node.nextSibling.previousSibling = node.previousSibling;
        }

        // update child refs
        if (this.firstChild == node) {
            this.setFirstChild(node.nextSibling);
        }
        if (this.lastChild == node) {
            this.setLastChild(node.previousSibling);
        }
        node.parentNode = null;
        node.previousSibling = null;
        node.nextSibling = null;
        return node;
    },
    indexOf: function(child) {
        return this.childNodes.indexOf(child);
    },
    bubble: function(fn, scope, args) {
        var p = this;
        while (p) {
            if (fn.apply(scope || p, args || [p]) === false) {
                break;
            }
            p = p.parentNode;
        }
    },

    cascade: function(fn, scope, args) {
        if (fn.apply(scope || this, args || [this]) !== false) {
            var cs = this.childNodes;
            for (var i = 0, len = cs.length; i < len; i++) {
                cs[i].cascade(fn, scope, args);
            }
        }
    },
    findChildBy: function(fn, scope) {
        var cs = this.childNodes;
        for (var i = 0, len = cs.length; i < len; i++) {
            if (fn.call(scope || cs[i], cs[i]) === true) {
                return cs[i];
            }
        }
        return null;
    }
}

var Load = function(pnode, data) {
    if (isArray(data)) {
        for (var d in data) {
            Load(pnode, data[d]);
        }
    }
    else {
        var node = createNode(data);
        pnode.appendChild(node);
        if (data.children) {
            Load(node, data.children);
        }
    }

}

var createNode = function(data) {
    var node = new Node();
    for (var m in data) {
        node[m] = data[m];
    }
    // node.childNodes = [];
    return node;
}

var findNode = function(root, id) {
    //    if (root.attributes && root.attributes.id == id) {
    //        return root;
    //    }  
    //    for (var i = 0; i < root.childNodes.length; i++) {
    //        var node = findNode(root.childNodes[i], id);
    //        if (!!node) return node;
    //    }
    var node;
    root.cascade(function(id) {
        if (this.attributes.id == id) {
            node = this;
            return false;
        }
    }, null, [id]);
    return node;
}