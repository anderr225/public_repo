(ns lab3.core)

(def h 0.1)

(defn get_trapezoid_area
  [f l r]
  (* (- r l)(/ (+ (f l) (f r)) 2)))

(defn get_sequence [f] (
  iterate (fn [[n result]] (
    let[
      next_v (inc n) 
      right (* h next_v)
      left (* n h)]            
        [next_v 
        (+ (get_trapezoid_area f left right) result)]))  
   [0 0])
)

(defn make_integral [f]
  (let [
    sequence (get_sequence f)
    integrate (fn [n] (second (nth sequence n)))]
    (fn [x]
      (let[
        n (int (/ x h))
        border (* h n)
      ]    
        (+ (integrate n) 
        (get_trapezoid_area f border x))
      )
    )
  )
)
