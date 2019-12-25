(ns lab4.core
  (:require
    [lab4.consts :refer :all]
    [lab4.variables :refer :all]
    [lab4.operations :refer :all]))

(defn replace-implication "replace implication with disjunction" [expr]
  (cond
    (constant? expr) expr
    (variable? expr) expr
    (negation? expr) (negation (replace-implication (first (args expr))))
    (conjunction? expr)
      (apply conjunction (map replace-implication (args expr)))
    (disjunction? expr)
      (apply disjunction (map replace-implication (args expr)))
    (implication? expr) 
      (disjunction
        (negation (replace-implication (second expr)))
        (replace-implication (second (next expr))))))

(defn push-negation "Use De Morgan's laws to push negation. Also remove multiple negation" [expr]
  (cond
    (constant? expr) expr
    (variable? expr) expr
    (negation? expr)
      (cond
        (constant? (first (args expr)))
          (if (same-constants? (first (args expr)) (constant 0)) (constant 1) (constant 0))
        (variable? (first (args expr))) expr
        (negation? (first (args expr)))
          (push-negation (first (args (first (args expr)))))
        (conjunction? (first (args expr)))
          (apply disjunction (map push-negation (map negation (args (first (args expr))))))    
        (disjunction? (first (args expr)))
          (apply conjunction (map push-negation (map negation (args (first (args expr)))))))
    (conjunction? expr)
      (apply conjunction (map push-negation (args expr)))
    (disjunction? expr)
      (apply disjunction (map push-negation (args expr)))
    (implication? expr)
      (apply implication (map push-negation (args expr)))))

(defn use-distribution  "Use distributive property of conjunction over disjunction" [expr]
  (cond
    (constant? expr) expr
    (variable? expr) expr
    (negation? expr)
      (negation (use-distribution (first (args expr))))
    (conjunction? expr)
      (let [
        a (use-distribution (first (args expr)))
        b (use-distribution (second (args expr)))] 
        (cond
          (disjunction? a)
            (disjunction (conjunction (first (args a)) b) (conjunction (second (args a)) b))
          (disjunction? b)
            (disjunction (conjunction a (first (args b))) (conjunction a (second (args b))))
        :default (conjunction a b)))
    (disjunction? expr)
      (apply disjunction (map use-distribution (args expr)))
    (implication? expr)
      (apply implication (map use-distribution (args expr)))))

(defn use-association  "Use associative property of conjunction and disjunction" [expr]
  (cond
    (constant? expr) expr
    (variable? expr) expr
    (negation? expr)
      (negation (use-association (first (args expr))))
    (conjunction? expr)
      (let [
        a (use-association (first (args expr)))
        b (use-association (second (args expr)))
        ] (cond
            (and (conjunction? a) (conjunction? b)) (apply conjunction (concat (args a) (args b)))
            (conjunction? a) (apply conjunction (concat (args a) (list b)))
            (conjunction? b) (apply conjunction (concat (list a) (args b)))
            :default (conjunction a b)))
;      (let [
;        conjunctions (filter (fn [x] (conjunction? x)) (args expr))
;        not_conjunctions (filter (fn [x] (not (conjunction? x))) (args expr))]
;          (apply conjunction 
;            (concat 
;              (reduce (fn [a b] (concat a (args b))) [] (map use-association conjunctions)) 
;              not_conjunctions)))
    (disjunction? expr)
;      (let [
;        disjunctions (filter (fn [x] (disjunction? x)) (args expr))
;        not_disjunctions (filter (fn [x] (not (disjunction? x))) (args expr))]
;          (apply disjunction 
;            (concat 
;              (reduce (fn [a b] (concat a (args b))) [] (map use-association disjunctions)) 
;              not_disjunctions)))
      (let [
        a (use-association (first (args expr)))
        b (use-association (second (args expr)))
        ] (cond
          (and (disjunction? a) (disjunction? b)) (apply disjunction (concat (args a) (args b)))
            (disjunction? a) (apply disjunction (concat (args a) (list b)))
          (disjunction? b) (apply disjunction (concat (list a) (args b)))
          :default (disjunction a b) ))
    (implication? expr)
      (apply implication (map use-association (args expr)))))

(defn clean-up  "Remove redundant constants. Simplify an expression" [expr]
  (cond
    (constant? expr) expr
    (variable? expr) expr
    (negation? expr)
      (negation (clean-up (first (args expr))))
    (conjunction? expr)
      (if 
        (boolean (some (fn [x] (and (constant? x) (same-constants? x (constant 0)))) (args expr)))
          (constant 0)
        (let [without_ones 
          (filter (fn [x] (not (and (constant? x) (same-constants? x (constant 1))))) (args expr))] 
            (cond 
              (empty? without_ones) (constant 1)
              (= 1 (count without_ones)) (first without_ones)
              :default (apply conjunction without_ones))))
    (disjunction? expr)
      (if 
        (boolean (some (fn [x] (and (constant? x) (same-constants? x (constant 1)))) (args expr)))
          (constant 1)
        (let [without_nulls (filter (fn [x] (not (and (constant? x) (same-constants? x (constant 0))))) (args expr))] 
          (cond 
            (empty? without_nulls) (constant 0)
            (= 1 (count without_nulls)) (first without_nulls)
          :default (apply disjunction without_nulls))))
    (implication? expr)
      (apply implication (map clean-up (args expr)))))

(defn dnf "Convertion to DNF" [expr]
  (clean-up (use-association (use-distribution (push-negation (replace-implication expr))))))

(defn apply-values "Apply values to an expression" [expr values]
  (cond
    (constant? expr) expr
    (variable? expr) (get values expr expr)
    (negation? expr) (negation (apply-values (first (args expr) values)))
    (conjunction? expr) (apply conjunction (map apply-values (args expr) values))
    (disjunction? expr) (apply disjunction (map apply-values (args expr) values))
    (implication? expr) (apply implication (map apply-values (args expr) values))))

(declare to-string)

(defn brackets [expr]
  (cond
    (or
      (constant? expr)
      (variable? expr))
        (to-string expr)
    (negation? expr)
      (str "~" (brackets (first (args expr))))
    :default
      (str "(" (to-string expr) ")")))

(defn to-string [expr]
  (cond
    (constant? expr)
      (str (first (args expr)))
    (variable? expr)
      (subs (str (first (args expr))) 1)
    (negation? expr)
      (str "~" (brackets (first (args expr))))
    (conjunction? expr)
      (clojure.string/join "&" (map brackets (args expr)))
    (disjunction? expr)
      (clojure.string/join "|" (map brackets (args expr)))
    (implication? expr)
      (clojure.string/join "->" (map brackets (args expr)))))
