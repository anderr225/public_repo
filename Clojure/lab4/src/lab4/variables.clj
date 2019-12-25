(ns lab4.variables)

(defn variable "Varialbe constructor" [name]
  {:pre [(keyword? name)]}
  (list ::var name))

(defn variable? "Type cheking for a variable" [expr]
  (= (first expr) ::var))

(defn variable-name "Get name of a variable" [v]
  (second v))

(defn same-variables? "Compare two variables" [v1 v2]
  (and
    (variable? v1)
    (variable? v2)
    (= (variable-name v1)
       (variable-name v2))))
