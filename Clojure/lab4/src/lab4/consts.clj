(ns lab4.consts)

(defn constant "Constant constructor" [value]
  {:pre [(or (= value 0) (= value 1))]}
  (list ::const value))

(defn constant? "Type checking for a constant" [expr]
  (= (first expr) ::const))

(defn constant-value "Get name of a constant" [c]
  (second c))

(defn same-constants? "Compare two constants" [c1 c2]
  (and
    (constant? c1)
    (constant? c2)
    (= (constant-value c1)
       (constant-value c2))))
