(ns lab2.core)

(defn get_trapezoid_area
  [f l r]
  (* (- r l)(/ (+ (f l) (f r)) 2))  
  )

(defn inner_integral
  [f l r h]
  (if
    (>= l r)
    0
    (+ 
      (get_trapezoid_area f l (+ l h))
      (inner_integral f (+ l h) r h)))  
  )

(defn calculate_integral
  [f x]
  (if (>= x 0) 
    (
      let [h 0.1 border (* h (int (/ x h)))] 
      (+ (inner_integral f 0 border h) 
         (get_trapezoid_area f border x))
      )
    (throw (IllegalArgumentException. "x should be > 0")))
  )

(defn get_integration_fn [f]
  (fn [x] (calculate_integral f x)))

(calculate_integral (fn [x] (* x x)) 5)
