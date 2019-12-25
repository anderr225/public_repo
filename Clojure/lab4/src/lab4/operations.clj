(ns lab4.operations
  (:require
    [lab4.consts :refer :all]
    [lab4.variables :refer :all]))

(defn conjunction "Conjunction constructor" [expr & rest]
  (cons ::conj (cons expr rest)))

(defn conjunction? "Type check for conjunction" [expr]
  (= (first expr) ::conj))

(defn disjunction "Disjunction constructor" [expr & rest]
  (cons ::disj (cons expr rest)))

(defn disjunction? "Type check for disjunction" [expr]
  (= (first expr) ::disj))

(defn implication "Implication constructor" [cause effect]
  (cons ::impl (list cause effect)))

(defn implication? "Type check for implication" [expr]
  (= (first expr) ::impl))

(defn negation "Negation constructor" [expr]
  (cons ::neg (list expr)))

(defn negation? "Type check for negation" [expr]
  (= (first expr) ::neg))

(defn args "Get arguments of an expression" [expr]
  (rest expr))
